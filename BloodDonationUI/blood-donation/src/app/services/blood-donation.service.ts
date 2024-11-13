import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BloodDonationService {
  private BaseUrl = 'https://localhost:7191/api';

  constructor(private http: HttpClient) {}

  getDonations(): Observable<any> {
    return this.http.get(`${this.BaseUrl}/donations`);
  }

  crearSolicitudDonacion(donation: any): Observable<any> {
    return from(this.http.post<any>(`${this.BaseUrl}/crear-solicitud-donacion`, donation));
  }
}
