import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListQuestionComponent } from "./features/list-question/list-question.component";

@Component({
  selector: 'app-root',
  imports: [ListQuestionComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Test';
}
