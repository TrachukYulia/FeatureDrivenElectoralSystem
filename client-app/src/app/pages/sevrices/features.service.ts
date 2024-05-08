import { Injectable } from '@angular/core';
import { Features } from '../models/features.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeaturesService {

  FormData!: Features;
  
  constructor(private http: HttpClient) {
  
   }

 addFeature(model: Features): Observable<void>
 {
   return this.http.post<void>('https://localhost:7059/api/Feature', model)
 }

 getFeatures(): Observable<any> {
  return this.http.get('https://localhost:7059/api/Feature');
}
updateFeature( id: number, featureData: any): Observable<any> {
  return this.http.put('https://localhost:7059/api/Feature/'+id, featureData);
}
deleteFeature(id: number): Observable<any> {
  return this.http.delete('https://localhost:7059/api/Feature/'+id);
}
}
