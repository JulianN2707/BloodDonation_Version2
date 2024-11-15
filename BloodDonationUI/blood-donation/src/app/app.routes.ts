// app.routes.ts
import { Routes } from '@angular/router';
import { DonationsComponent } from './components/donations/donations.component';
import { LoginComponent } from './components/login/login.component';
import { CreateUsuarioComponent } from './components/create-usuario/create-usuario.component';

export const routes: Routes = [
  { path: '', component: DonationsComponent }, // Ruta por defecto
  { path: 'login', component: LoginComponent },
  { path: 'create-usuario', component: CreateUsuarioComponent },
  // Otras rutas
];
