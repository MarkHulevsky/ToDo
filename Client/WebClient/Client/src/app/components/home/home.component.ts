import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToDoDirectoryService } from '../../services/todo-directory.service';
import { catchError, debounceTime, distinctUntilChanged, fromEvent, map, Observable, of, switchMap, tap } from 'rxjs';
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
import { NotifierService } from 'angular-notifier';
import { LoaderService } from '../../services/loader.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  toDoDirectories: ToDoDirectoryModel[] = [];
  selectedIndex = 0;
  @ViewChild('searchInput', { static: true }) searchInputRef!: ElementRef;

  constructor(
    private readonly _todoDirectoryService: ToDoDirectoryService,
    private readonly _pdfService: PdfService,
    private readonly _notifier: NotifierService,
    private readonly _loaderService: LoaderService,
    private readonly _router: Router) {
  }

  ngOnInit(): void {
    this._loaderService.show();
    this._loadDirectories()
      .pipe(
        tap((toDoDirectories) => {
          this.toDoDirectories = toDoDirectories;
          this._loaderService.hide();
        }),
        catchError((error) => {
          this._loaderService.hide();
          return of(error);
        })
      )
      .subscribe();

    this._registerSearchEvent();
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

  generatePdf(directoryId: string): void {
    this._loaderService.show();

    this._pdfService.generateByDirectoryId(directoryId)
      .pipe(
        tap((response) => {
          this._router.navigate(['pdf-preview', response.id]);
        }),
        catchError((error) => {
          this._loaderService.hide();
          return of(error);
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
      .pipe(
        switchMap(() => this._loadDirectories()),
        tap((toDoDirectories) => {
          this.toDoDirectories = toDoDirectories;
        })
      )
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

  private _registerSearchEvent(): void {
    fromEvent(this.searchInputRef.nativeElement, 'keyup')
      .pipe(
        map((value) => {
          const event = value as KeyboardEvent;
          return (event.target as HTMLInputElement).value
        }),
        debounceTime(500),
        distinctUntilChanged(),
        switchMap((value) => this._loadDirectories(value)),
        tap((toDoDirectories) => {
          this.toDoDirectories = toDoDirectories;
        })
      )
      .subscribe();
  }

  private _loadDirectories(searchText?: string): Observable<ToDoDirectoryModel[]> {
    const request: GetAllToDoDirectoriesByFilterRequest = {
      searchText: searchText
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
