export interface GetAllToDoNotesByDirectoryIdResponse {
  toDoNotes: GetAllToDoNotesByDirectoryIdResponseItemModel[];
}

export interface GetAllToDoNotesByDirectoryIdResponseItemModel {
  id: string;
  note: string;
  isCompleted: boolean;
}
