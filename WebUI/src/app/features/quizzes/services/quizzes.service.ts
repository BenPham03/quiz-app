import { Injectable } from '@angular/core';
import { AddQuizzesRequest } from '../models/add-quizzes-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QuizzesService {

  constructor(private http: HttpClient) { }
  addQuizzes(model: AddQuizzesRequest):Observable<void>{
    return this.http.post<void>('https://localhost:7282/api/Quizzes',model);
  }
  
}
