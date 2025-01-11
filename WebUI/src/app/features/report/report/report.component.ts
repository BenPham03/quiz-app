import { Component } from '@angular/core';
import { NavComponent } from "../../../core/components/nav/nav.component";
import { HeaderComponent } from "../../../core/components/header/header.component";

@Component({
  selector: 'app-report',
  imports: [NavComponent, HeaderComponent],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent {

}
