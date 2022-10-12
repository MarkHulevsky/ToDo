import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  template: './logout.component.html'
})
export class LogoutComponent implements OnInit {

  constructor(
    readonly _authService: AuthService,
    readonly _router: Router
  ) { }

  ngOnInit(): void {
    this._authService.logout();
    this._router.navigate(['']);
  }
}
