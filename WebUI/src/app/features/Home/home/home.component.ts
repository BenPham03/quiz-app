import { Component } from '@angular/core';
import { HeaderComponent } from "../../../core/components/header/header.component";
import { NavComponent } from "../../../core/components/nav/nav.component";
import { HomeService } from '../services/home.service';
import { Quiz } from '../models/Quiz';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { CreateInteractionRequest, Interaction } from '../models/Interaction';

@Component({
  selector: 'app-home',
  imports: [HeaderComponent, NavComponent, CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  quizzes: Quiz[] = [];
  pageIndex: number = 1;
  pageSize: number = 6;
  status: boolean = true;
  isDescending: boolean = true;
  isModalVisible = false;
  currentQuizId: string = '';
  userName : string = '';
  currentDuration : number = 0
  constructor(private homeService: HomeService,
    private router : Router
  ) { }

  ngOnInit() :void{
    this.loadNewQuizzes();
  }

  loadNewQuizzes(): void{
    this.homeService.getNew()
    .subscribe(
      (res) =>{
        this.quizzes = res.items
        console.log(res.items)
      },
      (error) => console.error(error)
    )
  }

openModal(quizId: string, config: string) {

  this.currentQuizId = quizId;
  const jsonObject = JSON.parse(config);
  console.log(jsonObject.Time)

  const time = jsonObject.Time
  this.currentDuration = time;
  this.isModalVisible = true;
}

  closeModal() {
    this.isModalVisible = false;
  }

  submitRetry() {
    if (this.userName.trim()) {
      if (this.currentQuizId !== null) {
        console.log(this.currentDuration + typeof(this.currentDuration))
      this.router.navigate(['/doExam', this.currentQuizId], { queryParams: {duration: this.currentDuration, name: this.userName }});
      }
    } else {
      alert('Please enter a valid name.');
    }
  }

  handleOverlayClick(event: MouseEvent) {
  if ((event.target as HTMLElement).id === 'modal-overlay') {
    this.closeModal();
    }
  }
  addNewInteraction(quizId: string, interactions: Interaction[]){
    console.log(interactions.length)
    if(interactions.length !=0)
    {
      this.homeService.deleteInteraction(quizId)
      .subscribe(
        (res) =>{
          console.log(res)
          const quiz = this.quizzes.find((q) => q.id === quizId);
          if(quiz)
          {
            console.log(quiz.interactions)
            console.log(quizId)
             quiz.interactions = quiz.interactions.filter(c => c.id !== quiz.interactions[0].id);
          }
        },
        (error) => console.error(error)

      )
    }
    else{
let interaction : CreateInteractionRequest= {
      type : 0,
      quizzId : quizId
    }
    this.homeService.createInteraction(interaction)
    .subscribe(
      (res) => {
        let interaction : Interaction= {
          id : res.id,
          interactType :res.type,
          createdAt : res.createdAt,
          userId : res.quizId,
          quizzId: res.quizId
      }
      const quiz = this.quizzes.find((q) => q.id === quizId);
      if (quiz) {
        // Giả sử response trả về interaction mới, bạn có thể đẩy interaction vào danh sách
        quiz.interactions.push(interaction);
      }
        console.log(res)
      },
      (error) => console.error(error)
    )
    }
    
  }
}


