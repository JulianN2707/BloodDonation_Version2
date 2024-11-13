import { Component, OnInit, inject } from '@angular/core';
import { BloodDonationService } from '../../services/blood-donation.service';
import { CommonModule } from '@angular/common';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-donations',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './donations.component.html',
  styleUrls: ['./donations.component.css']
})
export class DonationsComponent implements OnInit {

  private readonly fb = inject(FormBuilder);
  donations: any[] = [];
  formulario!: any;

  constructor(private bloodDonationService: BloodDonationService) {}

  ngOnInit(): void {
    this.formulario = this.inicializarFormulario();
  }

  inicializarFormulario(): FormGroup {
    return this.fb.group({
      centroSaludId: ['', Validators.required],
      grupoSanguineo: ['', Validators.required],
      rh: ['', Validators.required]
    });
  }

  submitDonation(): void {
    if (this.formulario.valid) {
      console.log("FORMULARIO: ", this.formulario);

      const donationData = this.formulario.value;
      this.addDonation(donationData);
    } else {
      console.error("Formulario inválido");
    }
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
    this.bloodDonationService.crearSolicitudDonacion(donation).subscribe({
      next : (response : any) => {
        console.log('Donación agregada:', response);
      },
      error : (error : any) => {
        console.error('Error agregando la solicitud de donacion:', error);
      }
    })
  }
}
