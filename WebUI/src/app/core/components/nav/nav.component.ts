import { Component, Input } from '@angular/core';
import { AuthService } from '../../../features/Account/services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-nav',
  imports: [CommonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  @Input() userData: string = "";
  isHome : boolean = false;
  isReport : boolean = false;
  isLibrary : boolean = false;
  
  constructor(
    private router : Router
  ){}
  ngOnInit(){
    if(this.userData === "home")
    {
      this.isHome = true;
    }
    if(this.userData === "report")
    {
      this.isReport = true;
    }
    if(this.userData === "library")
    {
      this.isLibrary = true;
    }
  }
  navigateTo(page: string) {
    this.router.navigate([`/${page}`]);
  }
}
