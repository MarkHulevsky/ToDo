import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { ToDoNoteModel } from './models/todo-note-model';
import { map, Observable, of, tap } from 'rxjs';
import { ToDoNoteService } from '../../services/todo-note.service';
import {
  GetAllToDoNotesByDirectoryIdResponse
} from '../../models/todo-note/response/get-all-todo-notes-by-directory-id.response';
import { CreateToDoNoteRequest } from '../../models/todo-note/request/create-todo-note.request';
import { UpdateToDoNoteRequest } from '../../models/todo-note/request/update-todo-note.request';
import { PdfService } from '../../services/pdf.service';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrls: ['./notes-list.component.scss']
})
export class NotesListComponent implements OnChanges {
  @Input() directoryId?: string;
  @Output() downloadPdf: EventEmitter<string> = new EventEmitter<string>();

  toDoNotes: ToDoNoteModel[] = [];

  constructor(
    private readonly _toDoNoteService: ToDoNoteService
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['directoryId']?.currentValue) {
      this._loadNotes()
        .pipe(
          tap((notes) => {
            this.toDoNotes = notes;
          })
        )
        .subscribe();
    }
  }

  completionStatusChange(note: ToDoNoteModel): void {
    const request: UpdateToDoNoteRequest = {
      id: note.id!,
      note: note.note!,
      isCompleted: note.isCompleted
    };

    this._toDoNoteService.update(request)
      .subscribe();
  }

  saveNote(note: ToDoNoteModel): void {
    if (!note.note) {
      return;
    }

    note.isEditing = false;

    const request: CreateToDoNoteRequest = {
      note: note.note,
      toDoDirectoryId: note.toDoDirectoryId
    };

    this._toDoNoteService.create(request)
      .subscribe();
  }

  cancelEditing(note: ToDoNoteModel): void {
    if (note.id) {
      note.isEditing = false;
      return;
    }

    const indexOf = this.toDoNotes.indexOf(note);

    this.toDoNotes.splice(indexOf, 1);
  }

  addNote(): void {
    const note: ToDoNoteModel = {
      isEditing: true,
      toDoDirectoryId: this.directoryId!,
      isCompleted: false
    };

    this.toDoNotes.unshift(note);
  }

  onDownloadPdf(): void {
    this.downloadPdf.emit(this.directoryId);
  }

  private _loadNotes(): Observable<ToDoNoteModel[]> {
    if (!this.directoryId) {
      return of([]);
    }

    return this._toDoNoteService.getAllByDirectoryId(this.directoryId)
      .pipe(
        map((response: GetAllToDoNotesByDirectoryIdResponse) =>
          response.toDoNotes.map((note) => ({
              id: note.id,
              note: note.note,
              isCompleted: note.isCompleted
            } as ToDoNoteModel)
          )
        )
      );
  }
}
