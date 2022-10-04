import { Injectable } from '@angular/core';
import { OAuthService} from 'angular-oauth2-oidc';
import { environment } from '../../environments/environment';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

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
    return this._oAuthService.hasValidAccessToken();
  }

  login(email: string, password: string): Observable<any> {
    return from(this._oAuthService.fetchTokenUsingPasswordFlow(email, password));
  }

  refresh(): Observable<any> {
    return from(
      this._oAuthService.refreshToken()
        .then(() => {
          return this._oAuthService.loadUserProfile();
        })
    );
  }

  logout(): void {
    this._oAuthService.logOut();
  }
}
