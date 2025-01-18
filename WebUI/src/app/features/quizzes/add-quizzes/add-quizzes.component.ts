import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddQuizzesRequest, Question } from '../models/add-quizzes-request.model';
import { QuizzesService } from '../services/quizzes.service';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-add-quizzes',
  imports: [FormsModule,CommonModule],
  templateUrl: './add-quizzes.component.html',
  styleUrl: './add-quizzes.component.css'
}
)
export class AddQuizzesComponent {
  examId!: string;
  isExamEditing = false;
  isEditing = false;
  model: AddQuizzesRequest;
  // Config object để xử lý trong modal
  config = {
    Time: 60, // thời gian làm bài tối đa (mặc định)
  };
  saveConfig() 
  {

    this.model.config = JSON.stringify(this.config);
    console.log(this.model.config)
  }
  constructor(private quizzesService: QuizzesService,private route: ActivatedRoute) 
  {
    this.model = {
      title: '',
      description: '',
      config: '',
      subject: '',
      questions: []
    };
    
  }
  ngOnInit() {
    this.examId = this.route.snapshot.paramMap.get('id')!;
    console.log('Exam ID:', this.examId); // Gọi API để lấy dữ liệu
    if (this.examId) {
      this.isEditing = true;
      this.loadExamData(this.examId);
      
    }
  }
  loadExamData(id: string) {
    this.quizzesService.getQuizById(id).subscribe((data) => {
      this.model = data; // Gán dữ liệu lấy được vào model
      console.log(this.model)
      this.config = JSON.parse( data.config) || this.config; // Nếu có config thì gán vào
      console.log(this.config)
    });
  }
  addQuestion() {
    this.model.questions.push({
      questionContent: '',
      questionType: 'Choice', // Mặc định là câu hỏi 1 đáp án
      answers: [
        { answerContent: '', isCorrect: false }, // Câu trả lời mặc định
      ],
    });
  }
  currentQuestion: Question = {
    questionContent: '',
    questionType: 'MultipleChoice',
    answers: [{ answerContent: '', isCorrect: false }],
  };
  editingIndex = -1;
  saveQuestion() {
    if (this.isEditing) {
      // Cập nhật câu hỏi đang chỉnh sửa
      this.model.questions[this.editingIndex] = { ...this.currentQuestion };
      this.isExamEditing = false;
      this.editingIndex = -1;
    } else {
      // Thêm mới câu hỏi
      this.model.questions.push({ ...this.currentQuestion });
    }
  
    // Reset current question sau khi lưu
    this.currentQuestion = {
      questionContent: '',
      questionType: 'Choice',
      answers: [{ answerContent: '', isCorrect: false }],
    };
  }
  
  toggleCorrectAnswer(index: number) {
    if (this.currentQuestion.questionType === 'Choice') {
      // Nếu là "single answer", chỉ cho phép 1 đáp án đúng
      this.currentQuestion.answers.forEach((answer, i) => {
        answer.isCorrect = i === index;
      });
    } else {
      // Nếu là "multi answer", checkbox hoạt động bình thường
      this.currentQuestion.answers[index].isCorrect =
        !this.currentQuestion.answers[index].isCorrect;
    }
  }

  // Thêm một câu trả lời vào câu hỏi cụ thể
  addAnswer() {
    this.currentQuestion.answers.push({ answerContent: '', isCorrect: false });
  }
  editQuestion(index: number) {
    this.currentQuestion = { ...this.model.questions[index] };
    this.isEditing = true;
    this.editingIndex = index;
  }

  // Xóa một câu hỏi
  deleteQuestion(index: number) {
    this.model.questions.splice(index, 1);
  }


  // Xóa một câu trả lời
  removeAnswer(answerIndex: number) {
    this.currentQuestion.answers.splice(answerIndex, 1);
  }
  // Lưu cấu hình vào trường config

  // Hàm xử lý submit form
  onFormSubmit() {
    console.log(this.isEditing)
    if (this.isEditing) {
      console.log(this.isEditing)
      if (this.examId !== null) {
        
        this.quizzesService.updateQuiz(this.examId, this.model).subscribe(() => {
          alert('Exam updated successfully!');
        });
      } else {
        alert('Invalid Exam ID!');
      }    
    } else {
      this.quizzesService.addQuizzes(this.model).subscribe(() => {
        alert('Exam created successfully!');
      });
    }
  }
}
