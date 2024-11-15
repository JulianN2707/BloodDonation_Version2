import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BloodDonationService {
  private BaseUrlDonacion = 'https://localhost:7191/api';
  private BaseUrlSolicitud = 'https://localhost:5021/api';
  private BaseUrlPersona = 'https://localhost:5169/api';

  constructor(private http: HttpClient) {}

  getDonations(): Observable<any> {
    return this.http.get(`${this.BaseUrlDonacion}/donations`);
  }

  crearDonante(request : any): Observable<any> {
    return this.http.post(`${this.BaseUrlSolicitud}/crear-solicitud-donante`, request);
  }

  obtenerSolicitudesDonante(): Observable<any> {
    return this.http.get(`${this.BaseUrlSolicitud}/obtener-solicitudesdonante`);
  }

  aprobarSolicitudDonante(request : any): Observable<any> {
    return this.http.post(`${this.BaseUrlSolicitud}/aprobar-donante`, request);
  }
  
  crearReservaDonacion(request : any): Observable<any> {
    return this.http.get(`${this.BaseUrlDonacion}/crear-reserva-donacion`);
  }

  crearSolicitudDonacion(donation: any): Observable<any> {
    return from(this.http.post<any>(`${this.BaseUrlDonacion}/crear-solicitud-donacion`, donation));
  }
}
