import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { DoExamService } from '../services/do-exam.service';
import { Question } from '../models/question';
import { UserAnswer } from '../models/UserAnswers';
import { SubmitExamRequestVM } from '../models/SubmitExamRequestVM';
import { CreateAttemptRequest } from '../models/CreateAttemptRequest';
// import { TimerService } from '../../services/timer.service';   /*service ở trang cài đặt thời gian thi cho bộ đề */
// import { QuizService } from '../../services/timer.service';   /*service ở trang chọn đề thi để bắt đầu thi */

@Component({
  selector: 'app-do-exam',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './do-exam.component.html',
  styleUrl: './do-exam.component.css',
})
export class DoExamComponent {
  duration: number = 0;
  quizId: string = '';

  timer: number = 0;
  displayTime: string = '00:00';
  currentDate: string = '';
  currentTime: string = '';

  answeredQuestions: Set<number> = new Set<number>();
  marksQuestions: Set<number> = new Set<number>();

  questions: Question[] = [];
  userAnswers: UserAnswer[] = [];
  submitExamRequestVM: SubmitExamRequestVM | null = null;
  createAttemptRequest: CreateAttemptRequest = {
    Score: 4,
    AttemptAt: new Date(),
    Name: 'Phạm Tuấn Hà',
    Duration: 10,
    QuizzId: this.quizId,
  };

  remainingTime: number = 0; // Thời gian còn lại
  time: any; // Tham chiếu tới interval

  constructor(
    private router: Router,
    private doExamService: DoExamService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // this.timer = this.timerService.getTimer();
    // this.startCountdown();
    this.route.paramMap.subscribe((params) => {
      const id = params.get('quizId');
      if (id) {
        this.quizId = id;
        this.createAttemptRequest.QuizzId = id;
      }
      console.log('Quiz ID:', this.quizId);
    });
    this.route.queryParams.subscribe((params) => {
      const duration = params['duration'] ? +params['duration'] : null;
      if (duration) {
        this.duration = duration; // Ép kiểu về number
        this.createAttemptRequest.Name = params['name'];
      }

      if (this.duration) {
        this.remainingTime = this.duration * 60;
      }
    });
    this.startCountdown();
    this.loadQuestions();
    this.updateDateTime(); // Gọi lần đầu để hiển thị ngay khi tải trang
    setInterval(() => {
      this.updateDateTime(); // Gọi lại mỗi giây
    }, 1000); // Cập nhật mỗi 1000ms (1 giây)
  }

  startCountdown() {
    this.time = setInterval(() => {
      if (this.remainingTime && this.remainingTime > 0) {
        this.remainingTime--;
      } else {
        clearInterval(this.timer); // Dừng đếm ngược
        this.onSubmit(); // Gọi hàm khi đếm ngược kết thúc
      }
    }, 1000);
  }

  getMinutes(seconds: number): number {
    return Math.floor(seconds / 60); // Lấy phần nguyên của giây chia cho 60 để tính phút
  }

  getSeconds(seconds: number): number {
    return seconds % 60; // Lấy phần dư của giây chia cho 60 để tính giây
  }

  // Gọi service để load dữ liệu câu hỏi từ API
  loadQuestions(): void {
    this.doExamService.getQuestion(this.quizId).subscribe({
      next: (data) => {
        this.questions = data;
        console.log(this.questions);
      },
      error: (err) => {
        console.error('Failed to load questions', err);
      },
    });
  }

  updateDateTime() {
    const now = new Date();

    // Cấu hình cho ngày
    const dateOptions: Intl.DateTimeFormatOptions = {
      year: 'numeric',
      month: 'numeric',
      day: 'numeric',
    };
    this.currentDate = now.toLocaleDateString('en-US', dateOptions); // Lấy ngày (MM/DD/YYYY)

    // Cấu hình cho giờ
    const timeOptions: Intl.DateTimeFormatOptions = {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
    };
    this.currentTime = now.toLocaleTimeString('en-US', timeOptions); // Lấy giờ (HH:mm:ss)
  }

  scrollToQuestion(questionId: string) {
    // Lấy phần tử HTML của câu hỏi dựa trên ID
    const questionElement = document.getElementById(`question-${questionId}`);
    if (questionElement) {
      questionElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }

  getCorrectAnswer(question: any): number[] {
    // Nếu correctAnswer là number (Choice), thì chuyển thành mảng
    return Array.isArray(question.correctAnswer)
      ? question.correctAnswer
      : [question.correctAnswer];
  }

  markAnswered(
    questionNum: number,
    questionId: string,
    answerId: string
  ): void {
    if (!this.answeredQuestions.has(questionNum)) {
      this.answeredQuestions.add(questionNum);
    }
    const existingAnswerIndex = this.userAnswers.findIndex(
      (ans) => ans.QuestionId === questionId
    );
    if (existingAnswerIndex !== -1) {
      // Nếu có đáp án cũ, update đáp án cũ
      this.userAnswers[existingAnswerIndex].AnswerId = answerId;
    } else {
      this.userAnswers.push({ QuestionId: questionId, AnswerId: answerId });
    }
    console.log(this.userAnswers);
  }
  markQuestions(questionId: number): void {
    if (!this.marksQuestions.has(questionId)) {
      this.marksQuestions.add(questionId);
    } else {
      this.marksQuestions.delete(questionId);
    }
  }
  // Hàm xử lý sự kiện khi nhấn nút "Submit"
  onSubmit(): void {
    console.log('begin');
    this.createAttemptRequest.Duration =
      this.duration * 60 - this.remainingTime;
    this.submitExamRequestVM = {
      UserAnswers: this.userAnswers,
      Attempt: this.createAttemptRequest,
    };
    console.log(this.submitExamRequestVM);
    this.doExamService.submit(this.submitExamRequestVM).subscribe(
      (res) => {
        console.log(res);
        this.router.navigate(['/home']);
      },
      (error) => {
        console.log(error);
      }
    );
    // this.router.navigate(['/home']);  // Chuyển hướng đến trang reports
  }
  // Hàm xử lý sự kiện khi nhấn nút "Quizizz"
  onHome(): void {
    this.router.navigate(['/home']); // Chuyển hướng đến trang home
  }
}
