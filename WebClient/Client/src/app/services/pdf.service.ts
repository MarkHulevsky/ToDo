import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpEvent } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PdfService {
  private readonly _pdfUrl = `${environment.gatewayUrl}pdf/api/pdf/`;

  constructor(private readonly _http: HttpClient) { }

  getByDirectoryId(directoryId: string): Observable<HttpEvent<any>> {
    return this._http.get(`${this._pdfUrl}getByDirectoryId/${directoryId}`, {
      observe: 'events',
      reportProgress: true,
      responseType: 'blob' as 'json'
    });
  }
}
