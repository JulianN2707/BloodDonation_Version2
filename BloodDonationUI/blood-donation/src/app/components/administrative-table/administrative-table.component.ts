import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BloodDonationService } from '../../services/blood-donation.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-administrative-table',
  templateUrl: './administrative-table.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class AdministrativeTableComponent implements OnInit{

  guidEnfermero = '9FF25FE2-0A6E-4D3C-A346-CFDEEA651C93';
  guidPopayan ='90764425-B8BB-4316-AE66-A8B64068CB77';
  people: any[] = [];

  selectedPerson: any = null;
  mostrar = false;

  constructor(private router: Router, private donacionService: BloodDonationService) {}

  ngOnInit() {
    this.obtenerSolicitudesDonantes();
  }

  obtenerSolicitudesDonantes(){
    
    this.donacionService.obtenerSolicitudesDonante().subscribe(
      (response) => {
        response.data.forEach((solicitud : any) => {
          const data = {
            name : `${solicitud.personaPrimerNombre} ${solicitud.personaPrimerApellido}`,
            email : solicitud.personaCorreoElectronico,
            title : solicitud.tipoPersonaId == this.guidEnfermero ? 'Enfermero' : 'Usuario',
            status : solicitud.estadoSolicitudUsuario,
            department: solicitud.personaMunicipioDireccionId == this.guidPopayan ? 'Popayán' : 'No define',
            fechaCreacion : solicitud.fechaCreacion,
            documento : solicitud.personaNumeroDocumento,
            direccion : solicitud.personaDireccion,
            solicitudUsuarioId : solicitud.solicitudUsuarioId
          }
          this.people.push(data);
        });
      },
      (error) => {
        console.error('Error adding donation:', error);
      }
    );
    

    /*const data = {
      name : 'Osiris chupalo',
      email : 'osirisSeLaCome@gmail.com',
      title : 'Usuario',
      status : 'Aprobado',
      department: 'Popayán',
      fechaCreacion : '2024/12/11',
      documento : '484763662',
      direccion : 'calle 3 # 2-1'
    }
    this.people.push(data);
    */
  }

  selectPerson(person: any) {
    this.selectedPerson = person;
    this.mostrar = true; // Abre el modal
  }

  closeModal() {
    this.mostrar = false; // Cierra el modal
  }

  approve() {   
    const request ={
      solicitudUsuarioId : this.selectedPerson.solicitudUsuarioId
    };

    this.donacionService.aprobarSolicitudDonante(request).subscribe(
      (response) => {
        if(response.data == true){
          alert('Aprobado: ' + this.selectedPerson.name);
        }else{
          alert('Error aprobando a: ' + this.selectedPerson.name);
        }
      },
      (error) => {
        console.error('Error adding donation:', error);
      }
    );
    this.closeModal(); // Cierra el modal después de aprobar
  }

  reject() {
    alert('Rejected: ' + this.selectedPerson.name);
    this.closeModal(); // Cierra el modal después de rechazar
  }
}
