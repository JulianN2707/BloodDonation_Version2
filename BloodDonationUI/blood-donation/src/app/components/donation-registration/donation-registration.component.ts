import { Component, OnInit, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { BloodDonationService } from '../../services/blood-donation.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-donation-registration',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterModule],
  templateUrl: './donation-registration.component.html',
  styleUrl: './donation-registration.component.css'
})
export class DonationRegistrationComponent implements OnInit{

  private readonly fb = inject(FormBuilder);
  formulario!: any;

  constructor(
    private bloodDonationService: BloodDonationService
  ) {}

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
      const donationData = this.formulario.value;
      this.addDonation(donationData);
    } else {
      console.error('Formulario inválido');
    }
  }

  addDonation(donation: any): void {
    this.bloodDonationService.crearSolicitudDonacion(donation).subscribe({
      next: (response: any) => {
        console.log('Donación agregada:', response);
        window.location.reload()
      },
      error: (error: any) => {
        console.error('Error agregando la solicitud de donacion:', error);
      }
    });
  }

  volver(): void {
    window.location.href = '/';
  }
}
