import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddQuizzesRequest, Question } from '../models/add-quizzes-request.model';
import { QuizzesService } from '../services/quizzes.service';

@Component({
  selector: 'app-add-quizzes',
  imports: [FormsModule,CommonModule],
  templateUrl: './add-quizzes.component.html',
  styleUrl: './add-quizzes.component.css'
}
)
export class AddQuizzesComponent {
  model: AddQuizzesRequest;

  // Config object để xử lý trong modal
  config = {
    maxTime: 60, // thời gian làm bài tối đa (mặc định)
    allowRetry: false, // có cho phép làm lại hay không
    showAnswers: false, // có hiển thị đáp án sau khi làm hay không
  };
  saveConfig() {
    this.model.config = JSON.stringify(this.config);}
  constructor(private quizzesService: QuizzesService) {
    this.model = {
      title: '',
      description: '',
      config: '',
      subject: '',
      questions: []
    };
    
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
  isEditing = false;
  editingIndex = -1;
  saveQuestion() {
    if (this.isEditing) {
      // Cập nhật câu hỏi đang chỉnh sửa
      this.model.questions[this.editingIndex] = { ...this.currentQuestion };
      this.isEditing = false;
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
    console.log('Submitting model:', JSON.stringify(this.config));
    console.log('Submit Data: ', this )
    this.quizzesService.addQuizzes(this.model)
      .subscribe({
        next: (response) => {
          console.log("This is successful!");
        },
        error: (error) => {
          console.error("Error occurred:", error);
        }
      });
  }
}
