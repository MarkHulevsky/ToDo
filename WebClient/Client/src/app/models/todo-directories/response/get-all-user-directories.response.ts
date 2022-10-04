export interface GetAllUserDirectoriesResponse {
  directories: GetAllUserDirectoriesDirectoryResponseModel[];
}

export interface GetAllUserDirectoriesDirectoryResponseModel {
  name: string;
  toDoNotes: GetAllUserDirectoriesToDoNoteResponseModel[];
}

export interface GetAllUserDirectoriesToDoNoteResponseModel {
  isCompleted: boolean;
  note: string;
}
