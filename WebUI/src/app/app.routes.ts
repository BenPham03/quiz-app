import { Routes } from '@angular/router';
import { ExampleComponent } from './features/example/example/example.component';
import { exampleGuard } from './core/guards/example.guard';
import { ReportComponent } from './features/report/report/report.component';
import { AdminComponent } from './features/admin/admin/admin.component';

export const routes: Routes = [
    { path: "example", component: ExampleComponent, canActivate: [exampleGuard] },
    { path: "report", component: ReportComponent },
    { path: "admin", component: AdminComponent }
];
