// ng generate service services/example
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Example } from '../models/example';

@Injectable({
  providedIn: 'root'
})
export class ExampleService {

  constructor(private http:HttpClient) { }

  getExamples():Observable<Example[]>{
    return this.http.get<Example[]>('link')
  }
  
  getExampleById(id:string):Observable<Example>{
    return this.http.get<Example>('https://localhost:7164/api/v1/Example/'+id);
  }

  addExample(model:Example):Observable<void>{
    return this.http.post<void>('https://localhost:7164/api/v1/Example',model);
  }

  updateExample(id:string,model:Example):Observable<void>{
    return this.http.put<void>('https://localhost:7164/api/v1/Example/'+id,model);
  }

  deleteExample(id:string):Observable<void>{
    return this.http.delete<void>('https://localhost:7164/api/v1/Example/'+id)
  }
}
