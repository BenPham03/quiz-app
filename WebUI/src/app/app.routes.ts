import { Routes } from '@angular/router';
import { ExampleComponent } from './features/example/example/example.component';
import { exampleGuard } from './core/guards/example.guard';

export const routes: Routes = [
    {path:"example",component:ExampleComponent,canActivate:[exampleGuard]}
];
