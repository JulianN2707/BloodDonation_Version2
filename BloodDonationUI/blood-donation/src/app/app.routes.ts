// app.routes.ts
import { Routes } from '@angular/router';
import { DonationsComponent } from './components/donations/donations.component';
import { LoginComponent } from './components/login/login.component';
import { AdministrativeTableComponent } from './components/administrative-table/administrative-table.component';
import { CreateUsuarioComponent } from './components/create-usuario/create-usuario.component';
import { DonationRegistrationComponent } from './components/donation-registration/donation-registration.component';

export const routes: Routes = [
  { path: '', component: DonationsComponent }, // Ruta por defecto
  { path: 'login', component: LoginComponent },
  { path: 'administrative', component: AdministrativeTableComponent},
  { path: 'create-usuario', component: CreateUsuarioComponent },
  { path: 'registro-donacion', component: DonationRegistrationComponent },
  // Otras rutas
];
