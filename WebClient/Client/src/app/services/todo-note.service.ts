import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  GetAllToDoNotesByDirectoryIdResponse
} from '../models/todo-note/response/get-all-todo-notes-by-directory-id.response';
import { environment } from '../../environments/environment';
import { CreateToDoNoteRequest } from '../models/todo-note/request/create-todo-note.request';
import { UpdateToDoNoteRequest } from '../models/todo-note/request/update-todo-note.request';

@Injectable({
  providedIn: 'root'
})
export class ToDoNoteService {
  private readonly _todoNoteUrl = `${environment.gatewayUrl}todo/api/todonote/`;

  constructor(private readonly _http: HttpClient) { }

  create(request: CreateToDoNoteRequest): Observable<any> {
    return this._http.post(`${this._todoNoteUrl}create`, request);
  }

  getAllByDirectoryId(directoryId: string): Observable<GetAllToDoNotesByDirectoryIdResponse> {
    return this._http.get<GetAllToDoNotesByDirectoryIdResponse>(`${this._todoNoteUrl}getAllByDirectoryId/${directoryId}`)
  }


  update(request: UpdateToDoNoteRequest): Observable<any> {
    return this._http.post(`${this._todoNoteUrl}update`, request);
  }
}
