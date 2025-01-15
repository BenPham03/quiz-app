import { Component } from '@angular/core';
import { HeaderComponent } from "../../../core/components/header/header.component";
import { NavComponent } from "../../../core/components/nav/nav.component";
import { HomeService } from '../services/home.service';
import { Quiz } from '../models/Quiz';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [HeaderComponent, NavComponent, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  quizzes: Quiz[] = [];
  pageIndex: number = 1;
  pageSize: number = 6;
  status: boolean = true;
  isDescending: boolean = true;
  constructor(private homeService: HomeService) { }

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
}
