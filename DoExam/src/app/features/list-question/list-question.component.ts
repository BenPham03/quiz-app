import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
// import { TimerService } from '../services/timer.service';

@Component({
  selector: 'app-list-question',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list-question.component.html',
  styleUrls: ['./list-question.component.css'],
})
export class ListQuestionComponent {
  
  timer: number = 0;
  displayTime: string = '00:00';
  currentDate: string = '';
  currentTime: string = '';

  constructor(private router: Router,
              // private timerService: TimerService
  ) {}

  questions = [
    { id: 1, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 2, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 3, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 4, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 5, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 6, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 7, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 8, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 9, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 10, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 11, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 12, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 13, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 14, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 15, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 16, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 17, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 18, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 19, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 20, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 21, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 22, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 23, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 24, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 25, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 26, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 27, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 28, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 29, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
    { id: 30, content: 'Question Content', options: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], answered: false },
  ];

  ngOnInit() {
    // this.timer = this.timerService.getTimer();
    // this.startCountdown();

    this.updateDateTime(); // Gọi lần đầu để hiển thị ngay khi tải trang
    setInterval(() => {
      this.updateDateTime(); // Gọi lại mỗi giây
    }, 1000); // Cập nhật mỗi 1000ms (1 giây)
  }
  
  // startCountdown(): void {
  //   const interval = setInterval(() => {
  //     if (this.timer > 0) {
  //       this.timer--;
  //       this.displayTime = this.formatTime(this.timer);
  //     } else {
  //       clearInterval(interval);
  //       alert('Time is up!');
  //     }
  //   }, 1000);
  // }

  // formatTime(seconds: number): string {
  //   const minutes = Math.floor(seconds / 60);
  //   const remainingSeconds = seconds % 60;
  //   return `${minutes.toString().padStart(2, '0')}:${remainingSeconds
  //     .toString()
  //     .padStart(2, '0')}`;
  // }

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
  

  scrollToQuestion(questionId: number) {
    // Lấy phần tử HTML của câu hỏi dựa trên ID
    const questionElement = document.getElementById(`question-${questionId}`);
    if (questionElement) {
      questionElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  } 
  
  onOptionSelected(questionId: number) {
    const question = this.questions.find(q => q.id === questionId);
    if (question) {
      question.answered = true; // Đánh dấu câu hỏi này đã được trả lời
    }
  }

    // Hàm xử lý sự kiện khi nhấn nút "Submit"
    onSubmit(): void {
      this.router.navigate(['/reports']);  // Chuyển hướng đến trang reports
    }

    // Hàm xử lý sự kiện khi nhấn nút "Quizizz"
    onHome(): void {
      this.router.navigate(['/home']);  // Chuyển hướng đến trang home
    }
}
