import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../../../services/account.service';
import { CreateUserRequest } from '../../../../models/account/request/create-user.request';
import { catchError, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  formGroup: FormGroup;
  isRegistrationFailed: boolean = false;

  get emailControl(): FormControl {
    return this.formGroup.get('email') as FormControl;
  }

  get nameControl(): FormControl {
    return this.formGroup.get('name') as FormControl;
  }

  get phoneNumberControl(): FormControl {
    return this.formGroup.get('phoneNumber') as FormControl;
  }

  get passwordControl(): FormControl {
    return this.formGroup.get('password') as FormControl;
  }

  get confirmPassword(): FormControl {
    return this.formGroup.get('confirmPassword') as FormControl;
  }

  confirmPasswordValidator: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    if (group.get('password')?.value === group.get('confirmPassword')?.value) {
      return null;
    }

    return { doNotMatch: true };
  };

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _accountService: AccountService,
    private readonly _router: Router) {

    this.formGroup = this._formBuilder.group({
        'email': ['', [Validators.email, Validators.required]],
        'name': ['', [Validators.required]],
        'phoneNumber': ['', [Validators.required]],
        'password': ['',
          [
            Validators.minLength(5),
            Validators.maxLength(13),
            Validators.required
          ]
        ],
        'confirmPassword': ['',
          [
            Validators.minLength(5),
            Validators.maxLength(13),
            Validators.required
          ]
        ]
      },
      {
        validators: this.confirmPasswordValidator
      });
  }

  onSubmit(): void {
    this.formGroup.markAllAsTouched();
    this.isRegistrationFailed = false;

    if (this.formGroup.invalid) {
      return;
    }

    const request: CreateUserRequest = {
      email: this.emailControl.value,
      password: this.passwordControl.value,
      phoneNumber: this.phoneNumberControl.value,
      userName: this.nameControl.value
    };

    this._accountService.register(request)
      .pipe(
        switchMap(() => this._router.navigate([''])),
        catchError(error => {
          this.isRegistrationFailed = true;
          return of(error);
        })
      )
      .subscribe();
  }
}
