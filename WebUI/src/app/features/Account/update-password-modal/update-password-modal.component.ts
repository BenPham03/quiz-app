import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { updatePasswordRequest } from '../model/uppdate-password';

@Component({
  selector: 'app-update-password-modal',
  imports: [FormsModule, CommonModule],
  templateUrl: './update-password-modal.component.html',
  styleUrl: './update-password-modal.component.css'
})
export class UpdatePasswordModalComponent {
  isModalOpen: boolean = false;
  confirmPassword: string =''
  updatePasswordRequest : updatePasswordRequest ={
    Email : '',
    NewPassword : ''
  }
  constructor(private authservice : AuthService){
  }

    openModal(email: string) {
    this.isModalOpen = true;
    this.updatePasswordRequest.Email = email
  }

  closeModal() {
    this.isModalOpen = false;
    this.confirmPassword = ''
  this.updatePasswordRequest ={
    Email : '',
    NewPassword : ''
  }
  }
  updatePassword() {
  if (this.confirmPassword === this.updatePasswordRequest.NewPassword) {
    this.authservice.updatePassword(this.updatePasswordRequest)
      .subscribe({
        next: (response) => {
          alert("Cập nhật mật khẩu thành công");
          this.closeModal();
        },
        error: (err) => {
          console.error('Error:', err);
          alert('Cập nhật mật khẩu thất bại!');
        }
      });
  } else {
    alert('Passwords do not match');
  }
}

}
