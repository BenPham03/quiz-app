import { Routes } from '@angular/router';
import {RouterModule} from '@angular/router';

import { exampleGuard } from './core/guards/example.guard';
import { QuizzesListComponent } from './features/quizzes/quizzes-list/quizzes-list.component';
import { AddQuizzesComponent } from './features/quizzes/add-quizzes/add-quizzes.component';

export const routes: Routes = [
    {
        path:"library",
        component:QuizzesListComponent,
        canActivate:[exampleGuard]
    },
    {
        path:"add-quizzes",
        component:AddQuizzesComponent,
        canActivate:[exampleGuard]
    }
];
