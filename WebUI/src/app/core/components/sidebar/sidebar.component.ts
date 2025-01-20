import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';
import { FormsModule } from '@angular/forms';
import {MatListModule} from '@angular/material/list';


@Component({
  selector: 'app-sidebar',
  imports: [RouterModule,MatSidenavModule,MatListModule,FormsModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  title = "Demo";
  opened =false;
}
