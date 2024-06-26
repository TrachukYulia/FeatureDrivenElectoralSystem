import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Item } from '../models/item.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  FormData!: Item;
  constructor(private http: HttpClient ) { }

  addItem(model: Item): Observable<void>
  {
    return this.http.post<void>('https://localhost:7059/api/Item', model)
  }
 
  getItems(): Observable<any> {
   return this.http.get('https://localhost:7059/api/Item');
 }
 getGeneticSolution(data: any): Observable<any> {
  return this.http.get('https://localhost:7059/genetic?'+data);
}
getGreedySolution(data: any): Observable<any> {
  return this.http.get('https://localhost:7059/greedy?'+data);
}
 updateItem( id: number, ItemData: any): Observable<any> {
   return this.http.put('https://localhost:7059/api/Item/'+id, ItemData);
 }
 deleteItem(id: number): Observable<any> {
   return this.http.delete('https://localhost:7059/api/Item/'+id);
 }
 exportItemsGreedy(): Observable<Blob> {
  return this.http.get(`https://localhost:7059/greedy/export`, { responseType: 'blob' });
}

exportItemsGenetic(): Observable<Blob> {
  return this.http.get(`https://localhost:7059/genetic/export`, { responseType: 'blob' });
}
}
