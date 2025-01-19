import { Component } from '@angular/core';
import { NavComponent } from '../../../core/components/nav/nav.component';
import { HeaderComponent } from '../../../core/components/header/header.component';
import { CommonModule } from '@angular/common';
import { Quiz } from '../models/Quiz';
import { HomeService } from '../services/home.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateInteractionRequest, Interaction } from '../models/Interaction';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  imports: [NavComponent, HeaderComponent, CommonModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.css',
})
export class SearchComponent {
  userData: string = 'home';
  quizzes: Quiz[] = [];
  pageIndex: number = 1;
  pageSize: number = 6;
  status: boolean = true;
  isDescending: boolean = true;
  isModalVisible = false;
  currentQuizId: string = '';
  userName: string = '';
  currentDuration: number = 0;
  quizId: string = '';

  constructor(
    private homeService: HomeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('quizId');
      console.log(id);
      if (id) {
        this.quizId = id;
      }
    });
    this.loadNewQuizzes();
  }

  loadNewQuizzes(): void {
    this.homeService.getItems(this.quizId).subscribe(
      (res) => {
        console.log(res);
        this.quizzes = res;
      },
      (error) => console.error(error)
    );
  }

  openModal(quizId: string, config: string) {
    this.currentQuizId = quizId;
    const jsonObject = JSON.parse(config);
    console.log(jsonObject.Time);
    const time = jsonObject.Time;
    this.currentDuration = time;
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  submitRetry() {
    if (this.userName.trim()) {
      if (this.currentQuizId !== null) {
        console.log(this.currentDuration + typeof this.currentDuration);
        this.router.navigate(['/doExam', this.currentQuizId], {
          queryParams: { duration: this.currentDuration, name: this.userName },
        });
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

  addNewInteraction(quizId: string, interactions: Interaction[]) {
    console.log(interactions.length);
    if (interactions.length != 0) {
      this.homeService.deleteInteraction(quizId).subscribe(
        (res) => {
          console.log(res);
          const quiz = this.quizzes.find((q) => q.id === quizId);
          if (quiz) {
            console.log(quiz.interactions);
            console.log(quizId);
            quiz.interactions = quiz.interactions.filter(
              (c) => c.id !== quiz.interactions[0].id
            );
          }
        },
        (error) => console.error(error)
      );
    } else {
      let interaction: CreateInteractionRequest = {
        type: 0,
        quizzId: quizId,
      };
      this.homeService.createInteraction(interaction).subscribe(
        (res) => {
          let interaction: Interaction = {
            id: res.id,
            interactType: res.type,
            createdAt: res.createdAt,
            userId: res.quizId,
            quizzId: res.quizId,
          };
          const quiz = this.quizzes.find((q) => q.id === quizId);
          if (quiz) {
            quiz.interactions.push(interaction);
          }
          console.log(res);
        },
        (error) => console.error(error)
      );
    }
  }
}
