import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
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

  GetTop5Right(id:string,from?:Date,to?:Date):Observable<Question[]>{
    let params = new HttpParams();
    if (from) {
      params = params.set('from',new Date(from).toISOString() );
    }
    if (to) {
      params = params.set('to', new Date(to).toISOString());
    }
    return this.http.get<Question[]>(BASE_URL+'Report/top-5-right/'+id,{params})
  }

  GetTop5Wrong(id:string,from?:Date,to?:Date):Observable<Question[]>{
    let params = new HttpParams();
    if (from) {
      params = params.set('from',new Date(from).toISOString());
    }
    if (to) {
      params = params.set('to',new Date(to).toISOString());
    }
    return this.http.get<Question[]>(BASE_URL+'Report/top-5-wrong/'+id,{params})
  }

  GetRank(id:string,from?:Date,to?:Date):Observable<Rank[]>{
    let params = new HttpParams();
    if (from) {
      params = params.set('from',new Date(from).toISOString());
    }
    if (to) {
      params = params.set('to',new Date(to).toISOString());
    }
    return this.http.get<Rank[]>(BASE_URL+'Report/rank/'+id,{params}).pipe(
      map(data => 
        data.map(item => ({
          ...item,
          attemptAt: new Date(item.attemptAt) // Chuyển đổi attemptAt thành Date
        }))
      )
    )
  }

  GetAnalyst(id:string,from?:Date,to?:Date):Observable<Rank[]>{
    let params = new HttpParams();
    if (from) {
      params = params.set('from', new Date(from).toISOString());
    }
    if (to) {
      params = params.set('to', new Date(to).toISOString());
    }
    return this.http.get<Rank[]>(BASE_URL+'Report/analyst/'+id,{params}).pipe(
      map(data => 
        data.map(item => ({
          ...item,
          attemptAt: new Date(item.attemptAt) // Chuyển đổi attemptAt thành Date
        }))
      )
    )
  }
}
