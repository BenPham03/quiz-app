import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
// import { TimerService } from '../../services/timer.service';   /*service ở trang cài đặt thời gian thi cho bộ đề */
// import { QuizService } from '../../services/timer.service';   /*service ở trang chọn đề thi để bắt đầu thi */

@Component({
  selector: 'app-do-exam',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './do-exam.component.html',
  styleUrl: './do-exam.component.css'
})
export class DoExamComponent {

  timer: number = 0;
  displayTime: string = '00:00';
  currentDate: string = '';
  currentTime: string = '';

  constructor(private router: Router,
              // private timerService: TimerService
  ) {}

  questions = [
    { id: 1, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null },
    { id: 2, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null },
    { id: 3, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 4, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 5, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 6, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 7, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 8, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 9, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 10, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 11, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 12, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 13, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 14, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 15, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 16, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Choice', answered: false, correctAnswer: null as number | null},
    { id: 17, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 18, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 19, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 20, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 21, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 22, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] },
    { id: 23, content: 'Question Content', answer: ['Answer 1', 'Answer 2', 'Answer 3', 'Answer 4'], questionType: 'Multi', answered: false, correctAnswer: [] as number[] }
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
  
  // Hàm xử lý khi chọn đáp án và đánh dấu câu hỏi đã được trả lời
  onOptionSelected(questionId: number, index: number): void {
    const question = this.questions.find(q => q.id === questionId);
    if (question) {
      if (question.questionType === 'Choice') {
        // Cập nhật đáp án đúng đối với câu hỏi kiểu "Choice"
        question.correctAnswer = index;
      } else if (question.questionType === 'Multi') {
        // Đảm bảo correctAnswer là một mảng trước khi thao tác
        if (!Array.isArray(question.correctAnswer)) {
          question.correctAnswer = [];
        }
  
        if (question.correctAnswer.includes(index)) {
          // Nếu đã chọn, bỏ chọn
          question.correctAnswer = question.correctAnswer.filter(i => i !== index);
        } else {
          // Nếu chưa chọn, thêm vào
          question.correctAnswer.push(index);
        }
      }
  
      // Đánh dấu câu hỏi đã được trả lời
      question.answered = true;
    }
  }
  
  getCorrectAnswer(question: any): number[] {
    // Nếu correctAnswer là number (Choice), thì chuyển thành mảng
    return Array.isArray(question.correctAnswer) ? question.correctAnswer : [question.correctAnswer];
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
