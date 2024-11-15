import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BloodDonationService {
  private BaseUrlDonacion = 'https://localhost:7053';
  private BaseUrlSolicitud = 'http://localhost:7056';
  private BaseUrlPersona = 'https://localhost:7049';

  constructor(private http: HttpClient) {}

  getDonations(): Observable<any> {
    return this.http.get(`${this.BaseUrlDonacion}/donations`);
  }

  crearDonante(request: FormData): Observable<any> {
    const headers = new HttpHeaders();
    return this.http.post<File>(`${this.BaseUrlSolicitud}/crear-solicitud-donante`, request,{headers});
  }

  crearReservaDonacion(request : any): Observable<any> {
    return this.http.get(`${this.BaseUrlDonacion}/crear-reserva-donacion`);
  }

  crearSolicitudDonacion(donation: any): Observable<any> {
    return from(this.http.post<any>(`${this.BaseUrlDonacion}/crear-solicitud-donacion`, donation));
  }
}
