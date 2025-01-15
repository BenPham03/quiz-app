import { Component, OnInit } from '@angular/core';
import { HeaderAdminComponent } from "../../../core/components/header-admin/header-admin.component";
import { CommonModule } from '@angular/common';
import { AdminService } from '../services/admin.service';
import { User } from '../models/admin';

@Component({
  selector: 'app-admin',
  imports: [HeaderAdminComponent, CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent implements OnInit {
  userList: User[] = []
  onlineCount: number = 0;
  newCount: number = 0;
  constructor(private service: AdminService) {

  }

  ngOnInit(): void {
    this.service.getUserList().subscribe({
      next: (data: User[]) => {
        this.userList = data
        console.log(data)
      },
      error: (err) => {
        console.log(err)
      }
    })

    this.service.getNewUserCount().subscribe({
      next:(data:number)=>{
        this.newCount=data
      },
      error:(err)=>{
        console.log(err)
      }
    })

    this.service.getUserOnlineCount().subscribe({
      next:(data:number)=>{
        this.onlineCount=data
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }
}
