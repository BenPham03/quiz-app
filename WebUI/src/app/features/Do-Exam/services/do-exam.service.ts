import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { BASE_URL } from '../../../app.config';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class DoExamService {

  private apiUrl = 'https://localhost:7282/api/Quizzes';

  constructor(private http: HttpClient) {}

  // Lấy bộ đề và câu hỏi cho bộ đề theo quizId
  getQuizDetails(quizId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${quizId}`);
  }
}
