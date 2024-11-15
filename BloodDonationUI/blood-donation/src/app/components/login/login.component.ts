import { Component, inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BloodDonationService } from '../../services/blood-donation.service';
import { Router, RouterModule } from '@angular/router';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{


  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  formulario!: any;

  constructor(private bloodDonationService: BloodDonationService) {}

  ngOnInit(): void {
    const today = new Date();
    this.formulario = this.inicializarFormulario();
  }

  inicializarFormulario(): FormGroup {
    return this.fb.group({
      usuario: ['', [Validators.required, Validators.minLength(3)]],
      contrasena: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  submitDonation(): void {
    if (this.formulario.valid) {
      const credenciales = this.formulario.value;
      this.iniciarSesion(credenciales);
    } else {
      console.error("Formulario inválido");
    }
  }

  iniciarSesion(credenciales: any): void {
    console.log("CREDENCIALES: ", credenciales);
    if(credenciales.usuario == "enfermeranc@gmail.com" && credenciales.contrasena == "Colombia2024."){
      console.log("Inicio enfermero");
      this.router.navigate(['/registro-donacion']);
    }
    else if(credenciales.usuario == "admin@gmail.com" && credenciales.contrasena == "Colombia2024."){
      console.log("Inicio solicitante");
      this.router.navigate(['/administrative']);
    }
  }

  volver(): void {
    window.location.href = '/';
  }

}
