import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';

import { NotificationService } from 'src/app/core/services/notification.service';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-password-reset-request',
  templateUrl: './password-reset-request.component.html',
  styleUrls: ['./password-reset-request.component.css']
})
export class PasswordResetRequestComponent implements OnInit {

  private email!: string;
  form!: FormGroup;
  loading!: boolean;
  appTitle = environment.applicationName;

  constructor(private authService: AuthenticationService,
    private notificationService: NotificationService,
    private titleService: Title,
    private router: Router) { }

  ngOnInit() {
    this.titleService.setTitle(`${this.appTitle} - Password Reset Request`);

    this.form = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });

    this.form.get('email')?.valueChanges
      .subscribe((val: string) => { this.email = val.toLowerCase(); });
  }

  resetPassword() {
    this.loading = true;
    this.authService.passwordResetRequest(this.email)
      .subscribe(
        results => {
          this.router.navigate(['/auth/login']);
          this.notificationService.openSnackBar('Password verification mail has been sent to your email address.');
        },
        error => {
          this.loading = false;
          this.notificationService.openSnackBar(error.error);
        }
      );
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
