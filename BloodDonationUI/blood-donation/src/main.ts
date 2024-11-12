import { bootstrapApplication } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { appConfig } from './app/app.config';
import { DonationsComponent } from '../src/app/components/donations/donations.component';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(DonationsComponent, {
  providers: [
    provideHttpClient()
  ]
});
