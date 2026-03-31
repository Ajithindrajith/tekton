import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PetService } from './pet.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, ],
  template: `
    <h2>Add Pet Details</h2>

    <div>
      <input [(ngModel)]="name" placeholder="Pet Name">
      <input [(ngModel)]="type" placeholder="Type">
      <input type="number" [(ngModel)]="age" placeholder="Age">
      <input [(ngModel)]="owner" placeholder="Owner">
      <button (click)="submitPet()">Add Pet</button>
    </div>

    <hr>

    <h3>Pet List</h3>
    <ul>
      <li *ngFor="let pet of pets">
        {{pet.name}} - {{pet.type}} - {{pet.age}} - {{pet.owner}}
      </li>
    </ul>
  `
})
export class AppComponent {

  name = '';
  type = '';
  age = 0;
  owner = '';
  pets: any[] = [];

  constructor(private petService: PetService) {}

  ngOnInit() {
    this.loadPets();
  }

  loadPets() {
    this.petService.getPets().subscribe(data => {
      this.pets = data;
    });
  }

  submitPet() {
    const pet = {
      name: this.name,
      type: this.type,
      age: this.age,
      owner: this.owner
    };

    this.petService.addPet(pet).subscribe(() => {
      this.loadPets();
      this.name = '';
      this.type = '';
      this.age = 0;
      this.owner = '';
    });
  }
}