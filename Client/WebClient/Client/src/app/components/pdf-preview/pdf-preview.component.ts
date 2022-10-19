import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, switchMap, tap } from 'rxjs';
import { PdfService } from '../../services/pdf.service';
import { HttpEventType } from '@angular/common/http';
import { saveAs } from '@progress/kendo-file-saver';

@Component({
  selector: 'app-pdf-preview',
  templateUrl: './pdf-preview.component.html',
  styleUrls: ['./pdf-preview.component.scss']
})
export class PdfPreviewComponent implements OnInit {
  private fileId!: string;

  pdfSrc!: string;

  constructor(
    private readonly _route: ActivatedRoute,
    private readonly _pdfService: PdfService
  ) { }

  ngOnInit(): void {
    this._route.params
      .pipe(
        tap(params => {
          this.fileId = params['fileId'];
        }),
        switchMap(() => this._load())
      )
      .subscribe();
  }

  save(): void {
    saveAs(this.pdfSrc, 'Todo_list.pdf');
  }

  private _load(): Observable<any> {
    return this._pdfService.download(this.fileId)
      .pipe(
        tap((response) => {
          if (response.type !== HttpEventType.Response) {
            return;
          }

          const blob = new Blob([response.body], { type: 'application-octet-stream' });

          this.pdfSrc = URL.createObjectURL(blob);
        })
      )
  }
}
