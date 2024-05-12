import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CharacteristicsComponent } from '../characteristics/characteristics.component';
import { Characteristic } from '../models/characteristic.model';

@Injectable({
  providedIn: 'root'
})
export class CharacteristicsService {
  FormData!: Characteristic;
  
  constructor(private http: HttpClient) {
  
   }

 addCharacteristics(model: any): Observable<void>
 {
   return this.http.post<void>('https://localhost:7059/api/Characteristic', model)
 }

 getCharacteristics(): Observable<any> {
  return this.http.get('https://localhost:7059/api/Characteristic');
}

getById( id: number): Observable<any> {
  return this.http.get('https://localhost:7059/api/Characteristic/'+id);
}
updateCharacteristics( id: number, data: any): Observable<any> {
  return this.http.put('https://localhost:7059/api/Characteristic/'+id, data);
}
deleteCharacteristics(id: number): Observable<any> {
  return this.http.delete('https://localhost:7059/api/Characteristic/'+id);
}
}
