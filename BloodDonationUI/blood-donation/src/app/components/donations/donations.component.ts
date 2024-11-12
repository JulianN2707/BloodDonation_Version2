import { Component, OnInit } from '@angular/core';
import { BloodDonationService } from '../../services/blood-donation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-donations',
  standalone: true,
  imports: [CommonModule],  // Elimina HttpClientModule de aquí
  templateUrl: './donations.component.html',
  styleUrls: ['./donations.component.css']
})
export class DonationsComponent implements OnInit {
  donations: any[] = [];

  constructor(private bloodDonationService: BloodDonationService) {}

  ngOnInit(): void {
    //this.loadDonations();
  }

  loadDonations(): void {
    this.bloodDonationService.getDonations().subscribe(
      (data) => {
        this.donations = data;
      },
      (error) => {
        console.error('Error fetching donations:', error);
      }
    );
  }

  addDonation(donation: any): void {
    this.bloodDonationService.addDonation(donation).subscribe(
      (response) => {
        console.log('Donación agregada:', response);
        this.loadDonations(); // Recargamos la lista después de agregar
      },
      (error) => {
        console.error('Error adding donation:', error);
      }
    );
  }
}
