export interface GetAllUserDirectoriesResponse {
  toDoDirectories: GetAllUserDirectoriesDirectoryResponseModel[];
}

export interface GetAllUserDirectoriesDirectoryResponseModel {
  id: string;
  name: string;
}
