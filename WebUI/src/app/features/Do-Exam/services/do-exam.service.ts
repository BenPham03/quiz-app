import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { BASE_URL } from '../../../app.config';
import { CookieService } from 'ngx-cookie-service';
import { Base_URL } from '../../../app.config';
import { Question } from '../models/question';
import { SubmitExamRequestVM } from '../models/SubmitExamRequestVM';

@Injectable({
  providedIn: 'root'
})
export class DoExamService {


  constructor(private http: HttpClient) {}

  // Lấy bộ đề và câu hỏi cho bộ đề theo quizId
  getQuestion(quizId: string): Observable<Question[]> {
    let params = new HttpParams()
        .set('quizId', quizId);
    return this.http.get<Question[]>(`${Base_URL}/Question/get-by-quizId`,{params});
  }
  submit(submitExamRequestVM : SubmitExamRequestVM): Observable<any> {
    return this.http.post(`${Base_URL}/DoExam/submit-exam`, submitExamRequestVM)
  }
}
