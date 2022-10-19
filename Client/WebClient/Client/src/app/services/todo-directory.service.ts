import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetAllUserDirectoriesResponse } from '../models/todo-directories/response/get-all-user-directories.response';
import { environment } from '../../environments/environment';
import {
  GetAllToDoDirectoriesByFilterRequest
} from '../models/todo-directories/request/get-all-todo-directories-by-filter.request';
import { CreateToDoDirectoryRequest } from '../models/todo-directories/request/create-todo-directory.request';

@Injectable({
  providedIn: 'root'
})
export class ToDoDirectoryService {
  private readonly _todoDirectoryUrl: string = `${environment.gatewayUrl}todo/api/tododirectory/`;

  constructor(private readonly _http: HttpClient) { }

  create(request: CreateToDoDirectoryRequest): Observable<any> {
    return this._http.post(`${this._todoDirectoryUrl}create`, request);
  }

  getAllByFilter(request: GetAllToDoDirectoriesByFilterRequest): Observable<GetAllUserDirectoriesResponse> {
    return this._http.post<GetAllUserDirectoriesResponse>(`${this._todoDirectoryUrl}getAllByFilter`, request);
  }
}
