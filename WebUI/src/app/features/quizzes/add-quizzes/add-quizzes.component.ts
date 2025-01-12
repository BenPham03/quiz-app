import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddQuizzesRequest } from '../models/add-quizzes-request.model';
import { QuizzesService } from '../services/quizzes.service';

@Component({
  selector: 'app-add-quizzes',
  imports: [FormsModule],
  templateUrl: './add-quizzes.component.html',
  styleUrl: './add-quizzes.component.css'
})
export class AddQuizzesComponent {
  model: AddQuizzesRequest;
  constructor(private quizzesService:QuizzesService){
    this.model = {
      title: '',
      description: '',
      config: '',
      subject: '',
      questions:[]
    }
  }
  onFormSubmit(){ 
    this.quizzesService.addQuizzes(this.model)
    .subscribe({
      next: (response) => {
        console.log("This is successful!")
      },
      error: (error) =>{

      }
      
    })
  }
}
