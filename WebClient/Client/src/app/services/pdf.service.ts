import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { GeneratePdfByDirectoryIdResponse } from '../models/document/response/generate-pdf-by-directory-id.response';

@Injectable({
  providedIn: 'root'
})
export class PdfService {
  private readonly _pdfUrl = `${environment.gatewayUrl}pdf/api/pdf/`;

  constructor(private readonly _http: HttpClient) { }

  generateByDirectoryId(directoryId: string): Observable<GeneratePdfByDirectoryIdResponse> {
    return this._http.get<GeneratePdfByDirectoryIdResponse>(`${this._pdfUrl}generateByDirectoryId/${directoryId}`);
  }

  download(fileId: string): Observable<HttpEvent<any>> {
    return this._http.get(`${this._pdfUrl}downloadFile/${fileId}`, {
      observe: 'events',
      reportProgress: true,
      responseType: 'blob' as 'json'
    });
  }
}
