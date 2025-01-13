import { Component, OnInit } from '@angular/core';
import { NavComponent } from "../../../core/components/nav/nav.component";
import { HeaderComponent } from "../../../core/components/header/header.component";
import { Question, Quizz, Rank } from '../models/report';
import { ReportService } from '../services/report.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-report',
  imports: [NavComponent, HeaderComponent,CommonModule,FormsModule],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent implements OnInit {
  QuizzList:Quizz[]=[]
  Top5Right:Question[]=[]
  Top5Wrong:Question[]=[]
  Ranks:Rank[]=[]
  Analyst:Rank[]=[]

  idNow:string=''

  constructor(private service:ReportService){}

  ngOnInit(): void {
    this.service.GetQuizList().subscribe({
      next:(data:Quizz[])=>{
        this.QuizzList=data
        this.idNow=data[0].id
        
        this.loadData();
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }

  formatToMinutesSeconds(duration: number): string {
    const minutes = Math.floor(duration / 60); // Lấy phần phút
    const seconds = duration % 60; // Lấy phần giây
    return `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  }

  loadData():void{
    this.service.GetTop5Right(this.idNow).subscribe({
      next:(data:Question[])=>{
        console.log(data)
        this.Top5Right=data
      },
      error:(err)=>{
        console.log(err)
      }
    })

    this.service.GetTop5Wrong(this.idNow).subscribe({
      next:(data:Question[])=>{
        console.log(data)
        this.Top5Wrong=data
      },
      error:(err)=>{
        console.log(err)
      }
    })

    this.service.GetRank(this.idNow).subscribe({
      next:(data:Rank[])=>{
        console.log(data)
        this.Ranks=data
      },
      error:(err)=>{
        console.log(err)
      }
    })

    this.service.GetAnalyst(this.idNow).subscribe({
      next:(data:Rank[])=>{
        console.log(data)
        this.Analyst=data
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }
}
