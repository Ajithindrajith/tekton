import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PetService } from './pet.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent {

  name = '';
  type = 'Dog';
  age = 0;
  owner = '';
  pets: any[] = [];
  editingId: string | null = null;
  petImages: any = {
  'Dog': 'https://placedog.net/200/200',
  'Cat': 'https://loremflickr.com/200/200/cat',
  'Rabbit': 'https://loremflickr.com/200/200/rabbit',
  'Bird': 'https://loremflickr.com/200/200/bird'
};

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

    if (this.editingId) {
      this.petService.updatePet(this.editingId, pet).subscribe(() => {
        this.loadPets();
        this.resetForm();
      });
    } else {
      this.petService.addPet(pet).subscribe(() => {
        this.loadPets();
        this.resetForm();
      });
    }
  }

  editPet(pet: any) {
    this.name = pet.name;
    this.type = pet.type;
    this.age = pet.age;
    this.owner = pet.owner;
    this.editingId = pet.id;
  }

  deletePet(id: string) {
    if (confirm('Are you sure?')) {
      this.petService.deletePet(id).subscribe(() => {
        this.loadPets();
      });
    }
  }

  resetForm() {
    this.name = '';
    this.type = 'Dog';
    this.age = 0;
    this.owner = '';
    this.editingId = null;
  }

  getImage(type: string): string {
    return this.petImages[type] || this.petImages['Dog'];
  }
}