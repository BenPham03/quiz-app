import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Base_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  getNew(pageIndex: number = 1, pageSize: number = 6, status: boolean = true, isDescending: boolean = true) : Observable<any> 
  {
    let params = new HttpParams()
    .set('pageIndex', pageIndex.toString())
    .set('pageSize', pageSize.toString())
    .set('status', status.toString())
    .set('isDescending', isDescending.toString());
    return this.http.get(`${Base_URL}/Quiz/get-filter`,{params});
  }
}
