import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-administrative-table',
  templateUrl: './administrative-table.component.html',
  standalone: true,
  imports: [CommonModule],
})
export class AdministrativeTableComponent {
  people = [
    {
      name: 'John Doe',
      email: 'john.doe@gmail.com',
      title: 'Software Engineer',
      department: 'IT',
      status: 'Active',
      position: 'Senior',
      imageUrl: 'https://mdbootstrap.com/img/new/avatars/8.jpg',
    },
    {
      name: 'Alex Ray',
      email: 'alex.ray@gmail.com',
      title: 'Consultant',
      department: 'Finance',
      status: 'Onboarding',
      position: 'Junior',
      imageUrl: 'https://mdbootstrap.com/img/new/avatars/6.jpg',
    },
    {
      name: 'Kate Hunington',
      email: 'kate.hunington@gmail.com',
      title: 'Designer',
      department: 'UI/UX',
      status: 'Awaiting',
      position: 'Senior',
      imageUrl: 'https://mdbootstrap.com/img/new/avatars/7.jpg',
    },
  ];

  selectedPerson: any = null;
  mostrar = false;

  selectPerson(person: any) {
    this.selectedPerson = person;
    this.mostrar = true; // Abre el modal
  }

  closeModal() {
    this.mostrar = false; // Cierra el modal
  }

  approve() {
    alert('Approved: ' + this.selectedPerson.name);
    this.closeModal(); // Cierra el modal después de aprobar
  }

  reject() {
    alert('Rejected: ' + this.selectedPerson.name);
    this.closeModal(); // Cierra el modal después de rechazar
  }
}
