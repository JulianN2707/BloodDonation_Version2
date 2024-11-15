import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule], // Importa RouterModule aquí
  template: `
    <nav>
      <!-- Aquí puedes tener enlaces de navegación si es necesario -->
    </nav>
    <router-outlet></router-outlet>
  `,
})
export class AppComponent {}
