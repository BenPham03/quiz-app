import { Routes } from '@angular/router';
import { ExampleComponent } from './features/example/example/example.component';
import { exampleGuard } from './core/guards/example.guard';
<<<<<<< HEAD
import { LoginComponent } from './features/Account/login/login.component';
import { RegisterComponent } from './features/Account/register/register.component';

export const routes: Routes = [
    {path:"example",component:ExampleComponent,canActivate:[exampleGuard]},
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    }
=======

export const routes: Routes = [
    {path:"example",component:ExampleComponent,canActivate:[exampleGuard]}
>>>>>>> 8d4ab35077fd29093f53c5fa7eb6bc86003c1e80
];
