import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { SendEmailRequest } from '../model/send-email';
import { UpdatePasswordModalComponent } from '../update-password-modal/update-password-modal.component';

@Component({
  selector: 'app-forgot-password-modal',
  imports: [FormsModule, CommonModule, UpdatePasswordModalComponent],
  templateUrl: './forgot-password-modal.component.html',
  styleUrl: './forgot-password-modal.component.css',
})
export class ForgotPasswordModalComponent {
  isModalOpen: boolean = false;
  emailSend: SendEmailRequest;
  code: string = '';
  codeIndentity: string = '';
  isCodeSent: boolean = false;
  @ViewChild(UpdatePasswordModalComponent, { static: false })
  updatePasswordModal!: UpdatePasswordModalComponent;
  constructor(private authservice: AuthService) {
    this.emailSend = {
      Email: '',
      Subject: '',
      Message: '',
    };
  }
  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.code = '';
    this.codeIndentity = '';
    this.isCodeSent = false;
    this.emailSend = {
      Email: '',
      Subject: '',
      Message: '',
    };
    this.isModalOpen = false;
  }

  sendCode() {
    if (!this.emailSend.Email) {
      alert('Plese enter valid email!');
      return;
    }
    const code = this.generateCode(6);
    this.emailSend.Message = code;
    this.emailSend.Subject = 'Reset Password Quiz-App';
    this.codeIndentity = code;
    this.authservice.sendMail(this.emailSend).subscribe({
      next: (res) => {
        alert('Code has sent to your email!');
      },
      error: (err) => {
        console.error('Error:', err);
      },
    });
    this.isCodeSent = true;
  }
  generateCode(length: number): string {
    const char =
      'qwertyuiopasdfghjklzxcvbnmZXCVBNMASDFGHJKLQWERTYUIOP1234567890';
    let code = '';
    for (let i = 0; i < length; i++) {
      code += char[Math.floor(Math.random() * char.length)];
    }
    return code;
  }
  enterCode() {
    if (this.codeIndentity === this.code) {
      this.updatePasswordModal.openModal(this.emailSend.Email);
      this.closeModal();
    } else {
      alert('Wrong code!');
    }
  }
}
