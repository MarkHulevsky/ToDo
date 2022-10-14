import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './components/pages/auth/auth.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './components/home/home.component';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { NotesListComponent } from './components/notes-list/notes-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { NotifierModule, NotifierOptions } from 'angular-notifier';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { PdfPreviewComponent } from './components/pdf-preview/pdf-preview.component';

const notifierDefaultOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'right',
    },
    vertical: {
      position: 'top',
    },
  },
  theme: 'material',
  behaviour: {
    autoHide: 5000,
    onClick: 'hide',
    onMouseover: 'pauseAutoHide',
    showDismissButton: false,
    stacking: 4,
  }
};

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotesListComponent,
    NavbarComponent,
    PdfPreviewComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    OAuthModule.forRoot(),
    AppRoutingModule,
    NotifierModule.withConfig(notifierDefaultOptions),
    AuthModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    PdfViewerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
