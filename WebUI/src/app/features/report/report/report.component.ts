import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NavComponent } from "../../../core/components/nav/nav.component";
import { HeaderComponent } from "../../../core/components/header/header.component";
import { Question, Quizz, Rank } from '../models/report';
import { ReportService } from '../services/report.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SplitstringPipe } from "../../../core/pipes/splitstring.pipe";

@Component({
  selector: 'app-report',
  imports: [NavComponent, HeaderComponent, CommonModule, FormsModule, SplitstringPipe],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ReportComponent implements OnInit {
  QuizzList: Quizz[] = [];
  Top5Right: Question[] = [];
  Top5Wrong: Question[] = [];
  Ranks: Rank[] = [];
  Analyst: Rank[] = [];

  idNow: string = '';
  earliestEntry!: Rank;
  latestEntry!: Rank;
  chartWidth: number = 0;
  x: number = 800;
  from!:Date;
  to!:Date;

  constructor(private service: ReportService,private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.service.GetQuizList().subscribe({
      next: (data: Quizz[]) => {
        this.QuizzList = data
        this.idNow = data[0].id
        this.loadData();
      },
      error: (err) => {
        console.log(err)
      }
    })
  }

  loadData(): void {
    this.service.GetTop5Right(this.idNow,this.from,this.to).subscribe({
      next: (data: Question[]) => {
        this.Top5Right = data
      },
      error: (err) => {
        console.log(err)
      }
    })

    this.service.GetTop5Wrong(this.idNow,this.from,this.to).subscribe({
      next: (data: Question[]) => {
        this.Top5Wrong = data
      },
      error: (err) => {
        console.log(err)
      }
    })

    this.service.GetRank(this.idNow,this.from,this.to).subscribe({
      next: (data: Rank[]) => {
        this.Ranks = data
      },
      error: (err) => {
        console.log(err)
      }
    })

    this.service.GetAnalyst(this.idNow,this.from,this.to).subscribe({
      next: (data: Rank[]) => {
        this.Analyst = data
        this.loadChart()
      },
      error: (err) => {
        console.log(err)
      }
    })

  }

  loadChart(): void {
    this.earliestEntry = this.Analyst.reduce((earliest, current) =>
      current.attemptAt.getTime() < earliest.attemptAt.getTime() ? current : earliest
    );
    this.latestEntry = this.Analyst.reduce((latest, current) => {
      const currentTimeWithDuration = current.attemptAt.getTime() + current.duration * 1000; // Cộng duration (sang ms)
      const latestTimeWithDuration = latest.attemptAt.getTime() + latest.duration * 1000; // Cộng duration (sang ms)

      return currentTimeWithDuration > latestTimeWithDuration ? current : latest;
    });
    this.chartWidth = this.calculateMinutesDifference(this.earliestEntry.attemptAt.getTime(), this.latestEntry.attemptAt.getTime() + this.latestEntry.duration * 1000)
    this.x = 800 > this.chartWidth ? 800 / this.chartWidth : 1
    setTimeout(() => {
      this.cdr.detectChanges();
    }, 0);
  }


  formatDateIntl = (date: Date | number): string => {
    return new Intl.DateTimeFormat("en-GB", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
      hour12: false
    }).format(date);
  };
  calculateMinutesDifference(date1: Date | number, date2: Date | number): number {
    const time1 = typeof date1 === 'number' ? date1 : date1.getTime();
    const time2 = typeof date2 === 'number' ? date2 : date2.getTime();
    const differenceInMs = Math.abs(time1 - time2); // Lấy giá trị tuyệt đối của chênh lệch
    return Math.floor(differenceInMs / (1000 * 60)); // Chuyển từ ms sang phút
  };
  generateRow(): number {
    return Math.floor(Math.random() * 480) + 4;
  }
  formatToMinutesSeconds(duration: number): string {
    const minutes = Math.floor(duration / 60); // Lấy phần phút
    const seconds = duration % 60; // Lấy phần giây
    return `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  }
}

