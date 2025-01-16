import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/admin';
import { BASE_URL } from '../../../app.config';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  getUserList():Observable<User[]>{
    return this.http.get<User[]>(BASE_URL+"Admin/get-list");
  }
  getUserOnlineCount():Observable<number>{
    return this.http.get<number>(BASE_URL+'Admin/get-user-online-count');
  }
  getNewUserCount():Observable<number>{
    return this.http.get<number>(BASE_URL+"Admin/get-new-user-count");
  }
  deleteUser(id:string):Observable<boolean>{
    return this.http.delete<boolean>(BASE_URL+'Admin/delete-user/'+id)
  }
  sendMail(message:string):Observable<void>{
    return this.http.post<void>(BASE_URL+'Admin/send-mail/',{message})
  }
}
