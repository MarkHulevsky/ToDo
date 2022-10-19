import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../../services/auth.service';
import { catchError, of, switchMap, tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  formGroup: FormGroup;
  isLoginFailed: boolean = false;

  get emailControl(): FormControl {
    return this.formGroup.get('email') as FormControl;
  }

  get passwordControl(): FormControl {
    return this.formGroup.get('password') as FormControl;
  }

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _authService: AuthService,
    private readonly _router: Router) {

    this.formGroup = this._formBuilder.group({
      'email': ['', [Validators.email, Validators.required]],
      'password': ['', [Validators.minLength(5), Validators.maxLength(13), Validators.required]]
    });
  }

  onSubmit(): void {
    this.formGroup.markAllAsTouched();
    this.isLoginFailed = false;

    if (this.formGroup.invalid) {
      return;
    }

    this._authService.login(this.emailControl.value, this.passwordControl.value)
      .pipe(
        switchMap(() => this._router.navigate(['/home'])),
        catchError((error) => {
          this.isLoginFailed = true;
          return of(error);
        })
      )
      .subscribe();
  }
}
