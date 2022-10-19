import { Injectable } from '@angular/core';
import { OAuthService} from 'angular-oauth2-oidc';
import { environment } from '../../environments/environment';
import { BehaviorSubject, catchError, EMPTY, from, Observable, of, switchMap, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthorized$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private readonly _oAuthService: OAuthService) {
    _oAuthService.clientId = environment.identityConfiguration.clientId;
    _oAuthService.tokenEndpoint = environment.identityConfiguration.tokenEndpoint;
    _oAuthService.userinfoEndpoint = environment.identityConfiguration.userInfoEndpoint;
    _oAuthService.scope = environment.identityConfiguration.scope;
    _oAuthService.oidc = false;
  }

  getAccessToken(): string {
    return this._oAuthService.getAccessToken();
  }

  getRefreshToken(): string {
    return this._oAuthService.getRefreshToken();
  }

  isAccessTokenValid(): boolean {
    const hasValidAccessToken = this._oAuthService.hasValidAccessToken();
    this.isAuthorized$.next(hasValidAccessToken);
    return hasValidAccessToken;
  }

  login(email: string, password: string): Observable<any> {
    return from(this._oAuthService.fetchTokenUsingPasswordFlow(email, password))
      .pipe(
        tap(() => {
          this.isAuthorized$.next(true);
        })
      );
  }

  refresh(): Observable<any> {
    return from(this._oAuthService.refreshToken())
      .pipe(
        switchMap(() => this._oAuthService.loadUserProfile()),
        catchError(() => {
          return EMPTY;
        })
      );
  }

  logout(): void {
    this._oAuthService.logOut();
    this.isAuthorized$.next(false);
  }
}
