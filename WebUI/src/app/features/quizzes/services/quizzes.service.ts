import { Injectable } from '@angular/core';
import { AddQuizzesRequest } from '../models/add-quizzes-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QuizzesService {

  constructor(private http: HttpClient) { }
  private baseUrl = 'https://localhost:7282/api/Quizzes';
  addQuizzes(model: AddQuizzesRequest):Observable<void>{
    return this.http.post<void>(this.baseUrl,model);
  }
  getQuizById(id: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`);
  }
  updateQuiz(id: string, quizData: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, quizData);
  }
}
