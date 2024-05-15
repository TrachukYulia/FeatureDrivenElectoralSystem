import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { CoreService } from '../../core/components/core.service';
import { FeaturesService } from '../sevrices/features.service';
import { CharacteristicsService } from '../sevrices/characteristics.service';
import { ItemService } from '../sevrices/item.service';
import { Characteristic } from '../models/characteristic.model';
import { Item } from '../models/item.model';
import { CharacteristicFeatureMapping } from '../models/ch-feature-map.model';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { AddEditItemComponent } from '../items/components/add-edit-item/add-edit-item.component';
import { Subscription } from 'rxjs';
import { FeatureQueryService } from '../sevrices/feature-query.service';
@Component({
  selector: 'app-greedy-alg',
  standalone: true,
  imports: [RouterLink, FormsModule, MatDialogModule,
    MatButtonModule, MatFormField, MatIcon, ReactiveFormsModule, MatTableModule,
    MatPaginator, MatPaginatorModule, MatFormFieldModule, MatSnackBarModule, CommonModule,],
  templateUrl: './greedy-alg.component.html',
  styleUrl: './greedy-alg.component.css'
})
export class GreedyAlgComponent implements OnInit {
  itemName!: string;
  selectedFeatures: { [key: number]: number } = {};
  characteristics: Characteristic[] = [];
  features: any = {};
  items: any = [];
  displayedColumns: string[] = ['id', 'name'];;
  dataSource!: MatTableDataSource<any>;
  characteristicsMap: Map<number, string[]> = new Map<number, string[]>();
  characteristicFeatureMapping: CharacteristicFeatureMapping[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  //@Input() queryString!: string;
  private subscription!: Subscription;
  featureQuery: any[] = [];

  constructor(private _dialog: MatDialog,
    private _featureService: FeaturesService,
    private _charService: CharacteristicsService,
    private _coreService: CoreService,
    private _itemService: ItemService,
    private _featureQueryService: FeatureQueryService
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
   // this.getItemsList();
   this.featureQuery = [73, 76]
   const queryString = this.featureQuery.map(id => `id=${id}`).join('&');
   this.getItemsList(queryString);
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
      const featureItem = item.featureItem.map((x: { featureId: any; })=>x.featureId);
      const matchingFeatureIds = featureItem.filter((featureId: any) => {
        const a = this.features.some((feature: { characteristicsId: number; id: any; }) => feature.characteristicsId === characteristicId && feature.id === featureId);
        return a; 
      });
      if (matchingFeatureIds.length = 1) {
        const matchingFeachure = this.features.find((f: { id: any; })=> f.id == matchingFeatureIds);
        return matchingFeachure.featureName;
      }
      else if (matchingFeatureIds.length > 1) 
        {
          const matchingFeachure = this.features.find((f: { id: any; })=> f.id == matchingFeatureIds);
          return matchingFeachure[0].featureName;
        }
      else {
        return '-';
      } 
  }
  loadFeatures() {
    this._featureService.getFeatures().subscribe({
      next: (res) => {
        this.features = res;
        console.log('Features is load:', this.features)
        this.subscription = this._featureQueryService.selectedFeaturesChanged.subscribe(features => {
          //this.featureQuery = features;
          this.featureQuery = [73, 76]
          const queryString = this.featureQuery.map(id => `id=${id}`).join('&');
          this.getItemsList(queryString);
    });
      },
      error: console.log,
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
  
  getItemsList(queryString: string){
    this._itemService.getGreedySolution(queryString).subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.items = res;
        console.log('Item is loaded', res);
      },
      error: console.log,
    })
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
