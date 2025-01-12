import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AdminTablleComponent } from "./features/admin/admin-tablle/admin-tablle.component";

@Component({
  selector: 'app-root',
  imports: [AdminTablleComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'TestAdmin';
}
