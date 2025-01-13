import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question, Quizz, Rank } from '../models/report';
import { BASE_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  constructor(private http:HttpClient) { }

  GetQuizList():Observable<Quizz[]>{
    return this.http.get<Quizz[]>(BASE_URL+'Report/quizz-list')
  }

  GetTop5Right(id:string):Observable<Question[]>{
    return this.http.get<Question[]>(BASE_URL+'Report/top-5-right/'+id)
  }

  GetTop5Wrong(id:string):Observable<Question[]>{
    return this.http.get<Question[]>(BASE_URL+'Report/top-5-wrong/'+id)
  }

  GetRank(id:string):Observable<Rank[]>{
    return this.http.get<Rank[]>(BASE_URL+'Report/rank/'+id)
  }

  GetAnalyst(id:string):Observable<Rank[]>{
    return this.http.get<Rank[]>(BASE_URL+'Report/analyst/'+id)
  }
}
