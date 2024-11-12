import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BloodDonationService {
  private BaseUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) {}

  getDonations(): Observable<any> {
    return this.http.get(`${this.BaseUrl}/donations`);
  }

  addDonation(donation: any): Observable<any> {
    return this.http.post(`${this.BaseUrl}/donations`, donation);
  }
}
