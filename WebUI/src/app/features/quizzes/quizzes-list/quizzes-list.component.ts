import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-quizzes-list',
  templateUrl: './quizzes-list.component.html',
  imports:[CommonModule],
  styleUrls: ['./quizzes-list.component.css']
})
export class QuizzesListComponent implements OnInit {
  quizzes: any[] = [];
  totalItems = 0;
  pageSize = 5;
  currentPage = 1;
  totalPages = 1;
  pages: number[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadQuizzes();
  }

  loadQuizzes(page: number = 1): void {
    const apiUrl = `https://localhost:7282/api/Quizzes/quizzes?page=${page}&pageSize=${this.pageSize}`;
    this.http.get<any>(apiUrl).subscribe(response => {
      this.quizzes = response.data;
      this.totalItems = response.totalItems;
      this.totalPages = Math.ceil(this.totalItems / this.pageSize);
      this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
      this.currentPage = page;
    });
  }

  deleteQuiz(quizId: number): void {
    if (confirm('Are you sure you want to delete this quiz?')) {
      const deleteUrl = `https://localhost:7282/api/Quizzes/quizzes/${quizId}`;
      this.http.delete(deleteUrl).subscribe(() => {
        alert('Quiz deleted successfully!');
        this.loadQuizzes(this.currentPage);
      });
    }
  }
}
