import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import { catchError, from, Observable, switchMap, throwError } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private readonly _unauthorizedStatusCode = 401;

  constructor(
    private readonly _authService: AuthService,
    private readonly _router: Router
  ) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    request = this._setRequestAuthHeader(request);

    return next.handle(request)
      .pipe(
        catchError((error) => {
          if (!(error instanceof HttpErrorResponse) || error.status !== this._unauthorizedStatusCode) {
            return throwError(error);
          }

          const refreshToken = this._authService.getRefreshToken();

          if (!refreshToken) {
            this._authService.logout();

            return from(this._router.navigate([''], { replaceUrl: true }));
          }

          return this._authService.refresh()
            .pipe(
              switchMap(() => next.handle(this._setRequestAuthHeader(request)))
            ) as Observable<any>;
        })
      );
  }

  private _setRequestAuthHeader(request: HttpRequest<any>): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        'Authorization': `Bearer ${this._authService.getAccessToken()}`
      }
    });
  }
}
