import { Component } from '@angular/core';
import { ExampleService } from '../services/example.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-example',
  imports: [CommonModule],
  templateUrl: './example.component.html',
  styleUrl: './example.component.css',
  providers:[ExampleService]
})
export class ExampleComponent {
  constructor(private service:ExampleService){}
}
