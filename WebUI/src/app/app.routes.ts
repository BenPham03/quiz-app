import { Routes } from '@angular/router';
import { ExampleComponent } from './features/example/example/example.component';
import { exampleGuard } from './core/guards/example.guard';
import { LoginComponent } from './features/Account/login/login.component';
import { RegisterComponent } from './features/Account/register/register.component';
import { ReportComponent } from './features/report/report/report.component';
import { HomeComponent } from './features/Home/home/home.component';
import { DoExamComponent } from './features/Do-Exam/do-exam/do-exam.component';
import { AdminComponent } from './features/admin/admin/admin.component';
import { SearchComponent } from './features/Home/search/search.component';

export const routes: Routes = [
  { path: 'example', component: ExampleComponent, canActivate: [exampleGuard] },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  { path: 'report', component: ReportComponent },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'doExam/:quizId',
    component: DoExamComponent,
  },
  {
    path: 'search/:quizId',
    component: SearchComponent,
  },
  { path: 'report', component: ReportComponent },
  { path: 'admin', component: AdminComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }, // Trang mặc định là 'home'
  { path: '**', redirectTo: 'home', pathMatch: 'full' }, // Đường dẫn không hợp lệ điều hướng về 'home'
];
