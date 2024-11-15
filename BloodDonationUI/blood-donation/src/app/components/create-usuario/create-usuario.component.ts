import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BloodDonationService } from '../../services/blood-donation.service';



@Component({
  selector: 'app-create-usuario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './create-usuario.component.html',
  styleUrl: './create-usuario.component.css'
})
export class CreateUsuarioComponent implements OnInit{

  formulario!: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private donacionService: BloodDonationService) {}

  ngOnInit(): void {
    this.formulario = this.fb.group({
      NumeroDocumento: ['', [Validators.required, Validators.maxLength(255)]],
      FechaExpedicionDocumento: ['', Validators.required],
      PrimerApellido: [''],
      PrimerNombre: [''],
      SegundoApellido: [''],
      SegundoNombre: [''],
      CorreoElectronico: ['', Validators.email],
      Celular: ['', Validators.maxLength(40)],
      Direccion: [''],
      MunicipioDireccionId: [null],
      TipoPersonaId: [null],
      GrupoSanguineo: [''],
      FactorRh: [''],
      archivo: [null] 
    });
  }

  cancelar(){
    window.location.href = '/';
  }

  onSubmit(): void {
    if (this.formulario.valid) {
      //const formData = new FormData();
      const fileInput = this.formulario.get('archivo')?.value;
      
      if (fileInput) {
        const fileBlob = new Blob([fileInput], { type: fileInput.type });
        const archivo = {
          archivo : fileBlob,
          tipoArchivoId : "1",
          referenciaSolicitud: undefined
        }
        const request ={
          numeroDocumento : this.formulario.get('NumeroDocumento')?.value,
          fechaExpedicionDocumento : this.formulario.get('FechaExpedicionDocumento')?.value,
          primerApellido : this.formulario.get('PrimerApellido')?.value,
          primerNombre : this.formulario.get('PrimerNombre')?.value,
          segundoApellido : this.formulario.get('SegundoApellido')?.value,
          segundoNombre : this.formulario.get('SegundoNombre')?.value,
          correoElectronico : this.formulario.get('CorreoElectronico')?.value,
          celular : this.formulario.get('Celular')?.value,
          direccion : this.formulario.get('Direccion')?.value,
          municipioDireccionId : this.formulario.get('MunicipioDireccionId')?.value,
          tipoPersonaId : this.formulario.get('TipoPersonaId')?.value,
          grupoSanguineo : this.formulario.get('GrupoSanguineo')?.value,
          factorRh : this.formulario.get('FactorRh')?.value,
          archivos : [archivo]
        }

        console.log(request);
        this.crearDonante(request);
        
      }
    }
  }

  crearDonante(donante: any): void {
    this.donacionService.crearDonante(donante).subscribe(
      (response) => {
        console.log('reserva agregada:', response);
      },
      (error) => {
        console.error('Error adding donation:', error);
      }
    );
  }
}
