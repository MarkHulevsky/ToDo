import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetAllUserDirectoriesResponse } from '../models/todo-directories/response/get-all-user-directories.response';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TodoDirectoryService {
  private readonly _todoDirectory: string = `${environment.gatewayUrl}todo/api/tododirectory/`;

  constructor(private readonly _http: HttpClient) { }

  getAll(): Observable<GetAllUserDirectoriesResponse> {
    return this._http.get<GetAllUserDirectoriesResponse>(`${this._todoDirectory}getAll`);
  }
}
