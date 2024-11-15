import { CommonModule } from '@angular/common';
import { Component, OnInit, viewChild, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BloodDonationService } from '../../services/blood-donation.service';

export interface ArchivoSeleccionado {
  archivo: any;
  tipoArchivoId: any;
}

@Component({
  selector: 'app-create-usuario',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, FormsModule],
  templateUrl: './create-usuario.component.html',
  styleUrl: './create-usuario.component.css'
})
export class CreateUsuarioComponent implements OnInit {

  formulario!: FormGroup;
  listaArchivosSeleccionados: ArchivoSeleccionado[] = [];
  @ViewChild('fileInput') fileInput!: ElementRef;


  constructor(private fb: FormBuilder, private router: Router, private donacionService: BloodDonationService) { }

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

  cancelar() {
    window.location.href = '/';
  }

  onSubmit(): void {
    if (this.formulario.valid) {
      const formData = new FormData();

      // Agregar datos del formulario al FormData
      formData.append('numeroDocumento', this.formulario.get('NumeroDocumento')?.value);
      formData.append('fechaExpedicionDocumento', this.formulario.get('FechaExpedicionDocumento')?.value);
      formData.append('primerApellido', this.formulario.get('PrimerApellido')?.value);
      formData.append('primerNombre', this.formulario.get('PrimerNombre')?.value);
      formData.append('segundoApellido', this.formulario.get('SegundoApellido')?.value);
      formData.append('segundoNombre', this.formulario.get('SegundoNombre')?.value);
      formData.append('correoElectronico', this.formulario.get('CorreoElectronico')?.value);
      formData.append('celular', this.formulario.get('Celular')?.value);
      formData.append('direccion', this.formulario.get('Direccion')?.value);
      formData.append('municipioDireccionId', this.formulario.get('MunicipioDireccionId')?.value);
      formData.append('tipoPersonaId', this.formulario.get('TipoPersonaId')?.value);
      formData.append('grupoSanguineo', this.formulario.get('GrupoSanguineo')?.value);
      formData.append('factorRh', this.formulario.get('FactorRh')?.value);

      // Archivos
      if (this.listaArchivosSeleccionados.length > 0) {
        this.listaArchivosSeleccionados.forEach((archivo, index) => {
          formData.append(`archivos[${index}].archivo`, archivo.archivo);
          formData.append(`archivos[${index}].tipoArchivoId`, archivo.tipoArchivoId);
        });
      }

      console.log('FormData:', formData);
      this.crearDonante(formData); // Asegúrate de que 'crearDonante' acepte FormData como argumento
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
  archivoActual?: File;
  async EventoSeleccionarArchivo(event: Event) {
    console.log("ENTRO SELECCIONAR")
    this.archivoActual = undefined;
    /**Obtenemos el evento del dom y preguntamos si contiene archivos, si es false rompe el metodo */
    const inputTarget = event.target as HTMLInputElement;
    if (!inputTarget.files?.length) {
      return;
    }
    this.archivoActual = inputTarget.files[0];
    console.log(this.archivoActual + "actual")
    const archivoAux: ArchivoSeleccionado = {
      archivo: this.archivoActual,
      tipoArchivoId: "AA382A00-C492-4259-8833-36A0442FB23A",
    };
    this.listaArchivosSeleccionados.push(archivoAux);

  }
  triggerFileInput() {
    this.fileInput.nativeElement.click();
  }
  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const selectedFile = input.files[0];
      console.log('Archivo seleccionado:', selectedFile);
    }
  }
}
