import { Routes } from '@angular/router';
import { ExampleComponent } from './features/example/example/example.component';
import { exampleGuard } from './core/guards/example.guard';
import { LoginComponent } from './features/Account/login/login.component';
import { RegisterComponent } from './features/Account/register/register.component';
import { ReportComponent } from './features/report/report/report.component';
import { HomeComponent } from './features/Home/home/home.component';

export const routes: Routes = [
    {path:"example",component:ExampleComponent,canActivate:[exampleGuard]},
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {path:"report",component:ReportComponent},
    {
        path: 'home',
        component: HomeComponent
    }
];
