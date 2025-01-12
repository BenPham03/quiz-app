import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-tablle',
  imports: [FormsModule, CommonModule],
  templateUrl: './admin-tablle.component.html',
  styleUrl: './admin-tablle.component.css'
})
export class AdminTablleComponent {

  currentDate: string = '';
  currentTime: string = '';
  newEmailContent: string = '';  // Biến này sẽ được liên kết với textarea

  constructor(private router: Router ) {}
  onlineUsers = 60;
  newUsers = 60;

  users = [
    { email: 'user1@example.com', examCount: 5, examSave: 3, examLiked: 7, avatar: 'user-avatar-placeholder.png' },
    { email: 'user2@example.com', examCount: 10, examSave: 6, examLiked: 12, avatar: 'user-avatar-placeholder.png' },
    { email: 'user1@example.com', examCount: 5, examSave: 3, examLiked: 7, avatar: 'user-avatar-placeholder.png' },
    { email: 'user2@example.com', examCount: 10, examSave: 6, examLiked: 12, avatar: 'user-avatar-placeholder.png' },
    { email: 'user1@example.com', examCount: 5, examSave: 3, examLiked: 7, avatar: 'user-avatar-placeholder.png' },
    { email: 'user2@example.com', examCount: 10, examSave: 6, examLiked: 12, avatar: 'user-avatar-placeholder.png' },
    { email: 'user3@example.com', examCount: 15, examSave: 8, examLiked: 20, avatar: 'user-avatar-placeholder.png' }
  ];

  emails = [
    { name: 'Name1', content: 'Email Content 1' },
    { name: 'Name2', content: 'Email Content 2' },
    { name: 'Name3', content: 'Email Content 3' },
    { name: 'Name4', content: 'Email Content 4' },
    { name: 'Name5', content: 'Email Content 5' },
    { name: 'Name6', content: 'Email Content 6' },
    { name: 'Name7', content: 'Email Content 7' },
    { name: 'Name8', content: 'Email Content 8' },
    { name: 'Name9', content: 'Email Content 9' },
    { name: 'Name10', content: 'Email Content 10' },
    { name: 'Name11', content: 'Email Content 11' },
    { name: 'Name12', content: 'Email Content 12' },
    { name: 'Name13', content: 'Email Content 13' },
    { name: 'Name14', content: 'Email Content 14' },
    { name: 'Name15', content: 'Email Content 15' },
    { name: 'Name16', content: 'Email Content 16' },
    { name: 'Name17', content: 'Email Content 17' },
    { name: 'Name18', content: 'Email Content 18' },
    { name: 'Name19', content: 'Email Content 19' }
  ];

  ngOnInit() {
    this.updateDateTime(); // Gọi lần đầu để hiển thị ngay khi tải trang
    setInterval(() => {
      this.updateDateTime(); // Gọi lại mỗi giây
    }, 1000); // Cập nhật mỗi 1000ms (1 giây)
  }

  updateDateTime() {
    const now = new Date();
    
    // Cấu hình cho ngày
    const dateOptions: Intl.DateTimeFormatOptions = {
      year: 'numeric',
      month: 'numeric',
      day: 'numeric',
    };
    this.currentDate = now.toLocaleDateString('en-US', dateOptions);  // Lấy ngày (MM/DD/YYYY)
  
    // Cấu hình cho giờ
    const timeOptions: Intl.DateTimeFormatOptions = {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
    };
    this.currentTime = now.toLocaleTimeString('en-US', timeOptions);  // Lấy giờ (HH:mm:ss)
  }

  // Hàm xử lý sự kiện khi nhấn nút "Quizizz"
  onHome(): void 
  {
    this.router.navigate(['/home']);  // Chuyển hướng đến trang home
  }

  // Phương thức xóa nội dung email
  clearEmailContent(): void {
    this.newEmailContent = '';  // Xóa nội dung trong biến newEmailContent
  }
}
