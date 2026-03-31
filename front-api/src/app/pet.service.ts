import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PetService {

  private apiUrl = 'http://20.117.216.225:80/api/pets';

  constructor(private http: HttpClient) {}

  getPets() {
    return this.http.get<any[]>(this.apiUrl);
  }

  addPet(pet: any) {
    return this.http.post(this.apiUrl, pet);
  }
}