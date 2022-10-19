import { BaseModel } from '../../shared/models/base-model';
import { ToDoNoteModel } from '../../notes-list/models/todo-note-model';

export interface ToDoDirectoryModel extends BaseModel {
  name?: string;
  toDoNotes?: ToDoNoteModel[];
  isEditing: boolean;
}
