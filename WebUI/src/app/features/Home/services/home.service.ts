import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Base_URL } from '../../../app.config';
import { CreateInteractionRequest, Interaction } from '../models/Interaction';
import { CreateAttemptRequest } from '../../Do-Exam/models/CreateAttemptRequest';

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  constructor(private http: HttpClient) {}

  getNew(
    pageIndex: number = 1,
    pageSize: number = 4,
    status: boolean = true,
    isDescending: boolean = true
  ): Observable<any> {
    let params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString())
      .set('status', status.toString())
      .set('isDescending', isDescending.toString());
    return this.http.get(`${Base_URL}/Quiz/get-filter`, { params });
  }
  getAttemptsRecently(
    pageIndex: number = 1,
    pageSize: number = 6
  ): Observable<any> {
    let params = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get(`${Base_URL}/Attempt/get-most-recent`, { params });
  }
  createInteraction(interaction: CreateInteractionRequest): Observable<any> {
    console.log(interaction);
    return this.http.post(
      `${Base_URL}/Interaction/add-new-interaction`,
      interaction
    );
  }
  deleteInteraction(quizId: string): Observable<any> {
    let params = new HttpParams().set('quizId', quizId.toString());
    return this.http.delete(`${Base_URL}/Interaction/delete`, { params });
  }
  getQuizById(quiz: string): Observable<any> {
    let params = new HttpParams().set('id', quiz.toString());
    return this.http.get(`${Base_URL}/Quiz/get-by-id`, { params });
  }
  getItems(quiz: string): Observable<any> {
    console.log('id' + quiz);
    let params = new HttpParams().set('items', quiz.toString());
    return this.http.get(`${Base_URL}/Quiz/getItems`, { params });
  }
}
