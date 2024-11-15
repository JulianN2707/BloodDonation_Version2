import { Component, OnInit, inject } from '@angular/core';
import { BloodDonationService } from '../../services/blood-donation.service';
import { CommonModule } from '@angular/common';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-donations',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterModule],
  templateUrl: './donations.component.html',
  styleUrls: ['./donations.component.css']
})
export class DonationsComponent implements OnInit {

  private readonly fb = inject(FormBuilder);
  donations: any[] = [];
  formulario!: any;
  formularioReserva!:any;
  minDate!: string;

  constructor(private bloodDonationService: BloodDonationService) {}

  ngOnInit(): void {
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0]; // Formato AAAA-MM-DD
    this.formulario = this.inicializarFormulario();
    this.formularioReserva = this.inicializarFormularioReserva();
  }

  inicializarFormulario(): FormGroup {
    return this.fb.group({
      centroSaludId: ['', Validators.required],
      grupoSanguineo: ['', Validators.required],
      rh: ['', Validators.required]
    });
  }

  inicializarFormularioReserva(): FormGroup{
    return this.fb.group({
      fechaReserva: ['', Validators.required]
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

  submitReserva(): void {
    if (this.formularioReserva.valid) {
      const requestReservaDonacion = {
        personaId : "3EB55A17-2AFD-49CC-9D75-08DCE14DE84F",
        fechaDonacion : this.formularioReserva.get('fechaReserva')?.value
      }
      console.log(requestReservaDonacion);
      this.crearReservaDonacion(requestReservaDonacion);
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


  crearReservaDonacion(donation: any): void {
    this.bloodDonationService.crearReservaDonacion(donation).subscribe(
      (response) => {
        console.log('reserva agregada:', response);
      },
      (error) => {
        console.error('Error adding donation:', error);
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
