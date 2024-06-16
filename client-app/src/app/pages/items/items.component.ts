import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { CoreService } from '../../core/components/core.service';
import { FeaturesService } from '../sevrices/features.service';
import { CharacteristicsService } from '../sevrices/characteristics.service';
import { AddEditItemComponent } from './components/add-edit-item/add-edit-item.component';
import { ItemService } from '../sevrices/item.service';
import { Characteristic } from '../models/characteristic.model';
import { Item } from '../models/item.model';
import { CharacteristicFeatureMapping } from '../models/ch-feature-map.model';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
@Component({
  selector: 'app-items',
  standalone: true,
  imports: [RouterLink, FormsModule, MatDialogModule,
    MatButtonModule, MatFormField, MatIcon, ReactiveFormsModule, MatTableModule,
    MatPaginator, MatPaginatorModule, MatFormFieldModule, MatSnackBarModule, CommonModule, MatSortModule],
  templateUrl: './items.component.html',
  styleUrl: './items.component.css'
})
export class ItemsComponent implements OnInit{
  itemName!: string;
  selectedFeatures: { [key: number]: number } = {};
  characteristics: Characteristic[] = [];
  features: any = {};
  items: any = [];
  displayedColumns: string[] = ['id', 'name', 'action'];;
  dataSource!: MatTableDataSource<any>;
  characteristicsMap: Map<number, string[]> = new Map<number, string[]>();
  characteristicFeatureMapping: CharacteristicFeatureMapping[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private _dialog: MatDialog,
    private _featureService: FeaturesService,
    private _charService: CharacteristicsService,
    private _coreService: CoreService,
    private _itemService: ItemService
  ){}
  getHeaderRowDef(): string[] {
    console.log('Characteristic:', this.characteristics.map(ch=>ch.name))
    this.displayedColumns = this.displayedColumns.concat(this.characteristics.map(ch=>ch.name))
    console.log('displayedColumns:', this.displayedColumns)

    return this.displayedColumns;
  }
  getColumnDef(): string[] {
    const tempArr : string[] = ['id', 'name', 'action'];
    tempArr.forEach(i=> {
      this.displayedColumns.push(i)
    })
    console.log('Characteristic:', this.characteristics)
    this.characteristics.forEach(ch=> {
      this.displayedColumns.push(ch.name)
    })
    console.log('displayedColumns:', this.displayedColumns)
    return this.displayedColumns;
  }
  ngOnInit(): void {
    this.loadCharacteristics();
    this.loadFeatures();
    this.getItemsList();
  }
  loadCharacteristics() {
    this._charService.getCharacteristics().subscribe({
      next: (res) => {
        this.characteristics = res;
       console.log('Characteristics is load:', this.characteristics)
       this.characteristics.forEach(characteristic => {
        this.displayedColumns.push(characteristic.name);
      });
      },
      error: console.log,
    })
  }

  getItemValue(item: any, characteristicId: number): string {
    const featureIds = item.featureItem.map((x: { featureId: any; })=>x.featureId);
    const matchingFeatures = this.features
      .filter((f: any) => f.characteristicsId === characteristicId && featureIds.includes(f.id))
      .map((f: any) => f.featureName);
    if (matchingFeatures.length > 0) {
      return matchingFeatures.join(', '); 
    } else {
      return '-'; 
    }
  }
  loadFeatures() {
    this._featureService.getFeatures().subscribe({
      next: (res) => {
        this.features = res;
        console.log('Features is load:', this.features)
      },
      error: console.log,
    })
  }
  openAddEditItemForm(){
    const dialogRef = this._dialog.open(AddEditItemComponent);
    dialogRef.afterClosed().subscribe({
     next: (val) =>{
       if(val) {
         this.getItemsList();
       }
     }
    })
   }
   getCharacteristicsList() {
    this._charService.getCharacteristics().subscribe({
      next: (res) => {
        this.characteristics = res;
        console.log(res);
      },
      error: console.log,
    })
  }

  getFeaturesList() {
    this._featureService.getFeatures().subscribe({
      next: (res) => {
        this.features = res;
        console.log(res);
      },
      error: console.log,
    })
  }
  
  getItemsList(){
    this._itemService.getItems().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.items = res;
        this.dataSource.sortingDataAccessor = (item, property) => {
          switch (property) {
            case 'name': return item.name.toLowerCase(); // Преобразуйте текст в нижний регистр перед сравнением
            default: return item[property];
          }
        };
      },
      error: console.log,
    })
  }
  deleteItem(id: number)
  {
    this._itemService.deleteItem(id).subscribe({
      next: (res) => {
       this._coreService.openSnackBar('Item deleted!')
       this.getItemsList();
      },
      error: console.log,
    })
  }
  openEditForm(data: any){
    const dialogRef = this._dialog.open(AddEditItemComponent, {
      data, 
    });
    dialogRef.afterClosed().subscribe({
    next: (val) =>{
      if(val) {
        this.getItemsList();
      }
    }
   })
   }
   buildCharacteristicsMap(items: Item[]): void {
    items.forEach(item => {
      item.featureItem?.forEach(featureItem => {
        const characteristicId = this.getCharacteristicId(featureItem.featureId);
        if (characteristicId) {
          const characteristicValues = this.characteristicsMap.get(characteristicId);
          if (characteristicValues) {
            characteristicValues.push(this.getFeatureName(featureItem.featureId));
          } else {
            this.characteristicsMap.set(characteristicId, [this.getFeatureName(featureItem.featureId)]);
          }
        }
      });
    });
  }
  
  getFeatureValueForCharacteristic(item: Item, characteristicId: number): string[] {
    return this.characteristicsMap.get(characteristicId) || [];
  }

  getCharacteristicId(featureId: number): number | undefined {
    const mapping = this.characteristicFeatureMapping.find(m => m.featureId === featureId);
    return mapping ? mapping.characteristicId : undefined;
  }

  getCharacteristicName(characteristicId: number): string {
    const characteristic = this.characteristics.find(x => x.id == characteristicId);
    return characteristic ? characteristic.name : '';
  }

  getFeatureName(featureId: number): string {
    const feature = this.features.find((x: { id: number; }) => x.id == featureId);
    return feature ? feature.name : '';
  }
}
