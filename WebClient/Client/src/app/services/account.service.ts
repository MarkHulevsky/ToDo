import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CreateUserRequest } from '../models/account/request/create-user.request';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly _accountUrl: string = `${environment.gatewayUrl}users/api/account/`;

  constructor(private readonly _http: HttpClient) { }

  register(request: CreateUserRequest): Observable<any> {
    return this._http.post<any>(`${this._accountUrl}register`, request);
  }
}
