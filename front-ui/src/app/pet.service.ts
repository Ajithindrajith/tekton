import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PetService {

  private apiUrl = 'http://20.115.178.157:80/api/pets';

  constructor(private http: HttpClient) {}

  getPets() {
    return this.http.get<any[]>(this.apiUrl);
  }

  getPetById(id: string) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  addPet(pet: any) {
    return this.http.post(this.apiUrl, pet);
  }

  updatePet(id: string, pet: any) {
    return this.http.put(`${this.apiUrl}/${id}`, pet);
  }

  deletePet(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}