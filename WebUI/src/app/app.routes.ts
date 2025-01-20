import { Routes } from '@angular/router';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './features/Account/login/login.component';
import { RegisterComponent } from './features/Account/register/register.component';
import { ReportComponent } from './features/report/report/report.component';
import { HomeComponent } from './features/Home/home/home.component';
import { DoExamComponent } from './features/Do-Exam/do-exam/do-exam.component';
import { AdminComponent } from './features/admin/admin/admin.component';
import { SearchComponent } from './features/Home/search/search.component';
import { QuizzesListComponent } from './features/quizzes/quizzes-list/quizzes-list.component';
import { AddQuizzesComponent } from './features/quizzes/add-quizzes/add-quizzes.component';
import { authGuard } from './features/Account/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  { path: 'report', component: ReportComponent, canActivate: [authGuard] },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'doExam/:quizId',
    component: DoExamComponent,
  },
  {
    path: 'library',
    component: QuizzesListComponent,
    canActivate: [authGuard],
  },
  {
    path: 'add-quizzes',
    component: AddQuizzesComponent,
    canActivate: [authGuard],
  },
  {
    path: 'edit-exam/:id',
    component: AddQuizzesComponent,
    canActivate: [authGuard],
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
