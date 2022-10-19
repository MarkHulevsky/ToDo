import { BaseModel } from '../../shared/models/base-model';

export interface ToDoNoteModel extends BaseModel {
  isCompleted: boolean;
  note?: string;
  toDoDirectoryId: string;
  isEditing: boolean;
}
