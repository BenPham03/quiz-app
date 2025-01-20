import { Component, OnInit } from '@angular/core';
import { HeaderAdminComponent } from '../../../core/components/header-admin/header-admin.component';
import { CommonModule } from '@angular/common';
import { AdminService } from '../services/admin.service';
import { User } from '../models/admin';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../Account/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  imports: [HeaderAdminComponent, CommonModule, FormsModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css',
})
export class AdminComponent implements OnInit {
  userList: User[] = [];
  onlineCount: number = 0;
  newCount: number = 0;
  message: string = 'Write an email to announce to all users!';
  constructor(
    private service: AdminService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.service.getUserList().subscribe({
      next: (data: User[]) => {
        this.userList = data;
        console.log(data);
      },
      error: (err) => {
        console.log(err);
      },
    });

    this.service.getNewUserCount().subscribe({
      next: (data: number) => {
        this.newCount = data;
      },
      error: (err) => {
        console.log(err);
      },
    });

    this.service.getUserOnlineCount().subscribe({
      next: (data: number) => {
        this.onlineCount = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  sendMail() {
    this.service.sendMail(this.message).subscribe({
      next: (data: any) => {
        alert(data.message);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  deleteUser(id: string) {
    this.service.deleteUser(id).subscribe({
      next: (data: boolean) => {
        if (data) this.userList = this.userList.filter((u) => u.id != id);
        else alert('Cannot delete!');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
