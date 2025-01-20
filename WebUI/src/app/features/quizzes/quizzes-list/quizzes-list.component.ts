import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NavComponent } from '../../../core/components/nav/nav.component';
import { HeaderComponent } from '../../../core/components/header/header.component';
@Component({
  selector: 'app-quizzes-list',
  templateUrl: './quizzes-list.component.html',
  imports: [CommonModule, FormsModule, NavComponent, HeaderComponent],
  styleUrls: ['./quizzes-list.component.css'],
  encapsulation: ViewEncapsulation.Emulated, // Hoặc ShadowDom để cô lập hoàn toàn
})
export class QuizzesListComponent implements OnInit {
  constructor(private http: HttpClient, private router: Router) {}
  userData: string = 'library';
  quizzes: any[] = [];
  totalItems = 0;
  pageSize = 5;
  currentPage = 1;
  totalPages = 1;
  pages: number[] = [];
  filteredQuizzes: any[] = [];
  categories = [
    { label: 'All my content', value: 'all' },
    { label: 'Previously used', value: 'previouslyUsed' },
    { label: 'Liked by me', value: 'liked' },
    { label: 'Shared with me', value: 'shared' },
    { label: 'Created by me', value: 'createdByMe' },
  ];

  selectedCategory: string = 'all';
  searchQuery: string = '';

  ngOnInit(): void {
    this.loadQuizzes();
  }

  onCategoryChange(category: string): void {
    this.selectedCategory = category;
    this.filterQuizzes();
  }

  filterQuizzes(): void {
    this.filteredQuizzes = this.quizzes.filter((quiz) => {
      const matchesSearch =
        !this.searchQuery ||
        quiz.title.toLowerCase().includes(this.searchQuery.toLowerCase());

      if (this.selectedCategory === 'all') {
        return matchesSearch;
      }

      if (this.selectedCategory === 'createdByMe') {
        return matchesSearch && quiz.isCreatedByMe; // Giả định API trả về thuộc tính này
      }

      if (this.selectedCategory === 'liked') {
        return matchesSearch && quiz.isLikedByMe; // Giả định API trả về thuộc tính này
      }

      if (this.selectedCategory === 'shared') {
        return matchesSearch && quiz.isSharedWithMe; // Giả định API trả về thuộc tính này
      }

      if (this.selectedCategory === 'previouslyUsed') {
        return matchesSearch && quiz.isPreviouslyUsed; // Giả định API trả về thuộc tính này
      }

      return matchesSearch;
    });
  }

  onSearch(): void {
    this.filterQuizzes();
  }

  loadQuizzes(page: number = 1): void {
    const apiUrl = `https://localhost:7282/api/Quizzes?page=${page}&pageSize=${this.pageSize}`;
    this.http.get<any>(apiUrl).subscribe((response) => {
      this.quizzes = response.data;
      this.totalItems = response.totalItems;
      this.totalPages = Math.ceil(this.totalItems / this.pageSize);
      this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
      this.currentPage = page;
    });
  }

  deleteQuiz(id: string): void {
    if (confirm('Are you sure you want to delete this quiz?')) {
      const deleteUrl = `https://localhost:7282/api/Quizzes/${id}`;
      this.http.delete(deleteUrl).subscribe(() => {
        alert('Quiz deleted successfully!');
        this.loadQuizzes(this.currentPage);
      });
    }
  }

  updateQuiz(quizId: string) {
    // Điều hướng sang trang chỉnh sửa với quizId
    this.router.navigate(['/edit-exam', quizId]);
  }
}
