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
    'Dog': 'https://images.unsplash.com/photo-1633722715463-d30628cfc4c7?w=150&h=150&fit=crop',
    'Cat': 'https://images.unsplash.com/photo-1574158622682-e40e69881006?w=150&h=150&fit=crop',
    'Rabbit': 'https://images.unsplash.com/photo-1585110396000-c9ffd4c9b12e?w=150&h=150&fit=crop',
    'Bird': 'https://images.unsplash.com/photo-1552728089-54bdde28bef4?w=150&h=150&fit=crop'
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