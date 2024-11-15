// app.routes.ts
import { Routes } from '@angular/router';
import { DonationsComponent } from './components/donations/donations.component';
import { LoginComponent } from './components/login/login.component';
import { AdministrativeTableComponent } from './components/administrative-table/administrative-table.component';

export const routes: Routes = [
  { path: '', component: DonationsComponent }, // Ruta por defecto
  { path: 'login', component: LoginComponent },
  {path: 'administrative', component: AdministrativeTableComponent}
  // Otras rutas
];
