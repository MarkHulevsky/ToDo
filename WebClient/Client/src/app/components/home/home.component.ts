import { Component, OnInit } from '@angular/core';
import { ToDoDirectoryService } from '../../services/todo-directory.service';
import { map, Observable, tap } from 'rxjs';
import { ToDoDirectoryModel } from './models/todo-directory-model';
import {
  GetAllUserDirectoriesResponse
} from '../../models/todo-directories/response/get-all-user-directories.response';
import {
  GetAllToDoDirectoriesByFilterRequest
} from '../../models/todo-directories/request/get-all-todo-directories-by-filter.request';
import { CreateToDoDirectoryRequest } from '../../models/todo-directories/request/create-todo-directory.request';
import { HttpEventType } from '@angular/common/http';
import { saveAs } from '@progress/kendo-file-saver';
import { PdfService } from '../../services/pdf.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  toDoDirectories: ToDoDirectoryModel[] = [];
  searchText?: string;
  selectedIndex = 0;

  constructor(private readonly _todoDirectoryService: ToDoDirectoryService,
              private readonly _pdfService: PdfService) {}

  ngOnInit(): void {
    this._loadDirectories()
      .pipe(
        tap((toDoDirectories) => {
          this.toDoDirectories = toDoDirectories;
        })
      )
      .subscribe();
  }

  search(): void {
    this._loadDirectories()
      .pipe(
        tap((toDoDirectories) => {
          this.toDoDirectories = toDoDirectories;
        })
      )
      .subscribe();
  }

  downloadPdf(directoryId: string): void {
    this._pdfService.getByDirectoryId(directoryId)
      .pipe(
        tap((response) => {
          if (response.type !== HttpEventType.Response) {
            return;
          }

          const blob = new Blob([response.body], { type: 'application/pdf' });
          saveAs(blob, `TodoList.pdf`);
        })
      )
      .subscribe();
  }

  saveDirectory(directory: ToDoDirectoryModel): void {
    if (!directory.name) {
      return;
    }

    directory.isEditing = false;

    const createRequest: CreateToDoDirectoryRequest = {
      name: directory.name
    };

    this._todoDirectoryService.create(createRequest)
      .subscribe();
  }

  cancelEditing(directory: ToDoDirectoryModel): void {
    if (directory.id) {
      directory.isEditing = false;
      return;
    }

    const indexOf = this.toDoDirectories.indexOf(directory);

    this.toDoDirectories.splice(indexOf, 1);
  }

  addDirectory(): void {
    const directory: ToDoDirectoryModel = {
      isEditing: true
    };

    this.toDoDirectories.unshift(directory);
    this.selectedIndex++;
  }

  private _loadDirectories(): Observable<ToDoDirectoryModel[]> {
    const request: GetAllToDoDirectoriesByFilterRequest = {
      searchText: this.searchText
    };

    return this._todoDirectoryService.getAllByFilter(request)
      .pipe(
        map((response: GetAllUserDirectoriesResponse) =>
          response.toDoDirectories.map((toDoDirectory) => ({
              id: toDoDirectory.id,
              name: toDoDirectory.name,
              isEditing: false
            } as ToDoDirectoryModel)
          )
        )
      );
  }
}
