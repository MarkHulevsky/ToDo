import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Client';
  isAuthorized = true;

  constructor(private readonly _authService: AuthService) {
    _authService.isAuthorized$
      .subscribe((isAuthorized) => {
        this.isAuthorized = isAuthorized;
      });
  }
}
