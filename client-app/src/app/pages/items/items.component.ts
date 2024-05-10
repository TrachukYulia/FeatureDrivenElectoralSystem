import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { CoreService } from '../../core/components/core.service';
import { FeaturesService } from '../sevrices/features.service';
import { CharacteristicsService } from '../sevrices/characteristics.service';
import { AddEditItemComponent } from './components/add-edit-item/add-edit-item.component';
import { ItemService } from '../sevrices/item.service';
import { Characteristic } from '../models/characteristic.model';
import { Features } from '../models/features.model';
@Component({
  selector: 'app-items',
  standalone: true,
  imports: [RouterLink, FormsModule, MatDialogModule,
    MatButtonModule, MatFormField, MatIcon, ReactiveFormsModule, MatTableModule,
    MatPaginator, MatPaginatorModule, MatFormFieldModule, MatSnackBarModule],
  templateUrl: './items.component.html',
  styleUrl: './items.component.css'
})
export class ItemsComponent implements OnInit{
  itemName!: string;
  selectedFeatures: { [key: number]: number } = {};
  characteristics: Characteristic[] = [];
  // features: Features[] = []
  features: any = {};

  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private _dialog: MatDialog,
    private _featureService: FeaturesService,
    private _charService: CharacteristicsService,
    private _coreService: CoreService,
    private _itemService: ItemService
  ){}

  ngOnInit(): void {
    this.getCharacteristicsList();
    this.getFeaturesList();
    this.getItemsList();
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
  // ngOnInit(): void {
  //   throw new Error('Method not implemented.');
  // }
  getItemsList(){
    this._itemService.getItems().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        console.log(res);
      },
      error: console.log,
    })
  }
  deleteItem(id: number)
  {
    this._itemService.deleteItem(id).subscribe({
      next: (res) => {
       this._coreService.openSnackBar('Feature deleted!')
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
}
