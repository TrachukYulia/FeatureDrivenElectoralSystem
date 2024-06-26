import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';

import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { CoreService } from '../../core/components/core.service';
import { FeaturesService } from '../sevrices/features.service';
import { AddEditFeatureComponent } from './components/add-edit-feature/add-edit-feature.component';
import { CharacteristicsService } from '../sevrices/characteristics.service';
@Component({
  selector: 'app-features',
  standalone: true,
  imports: [   RouterLink,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatSortModule],
  templateUrl: './features.component.html',
  styleUrl: './features.component.css'
})
export class FeaturesComponent implements OnInit {
  displayedColumns: string[] = ['id', 'featureName', 'characteristicName',  'action'];
  dataSource!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private _dialog: MatDialog,
    private _featureService: FeaturesService,
    private _coreService: CoreService,
  ){}

  ngOnInit(): void {
    this.getFeaturesList();
  }
  openAddEditItemForm(){
   const dialogRef = this._dialog.open(AddEditFeatureComponent);
   dialogRef.afterClosed().subscribe({
    next: (val) =>{
      if(val) {
        this.getFeaturesList();
      }
    }
   })
  }

  getFeaturesList(){
    this._featureService.getFeatures().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
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
  deleteFeature(id: number)
  {
    this._featureService.deleteFeature(id).subscribe({
      next: (res) => {
       this._coreService.openSnackBar('Feature deleted!')
       this.getFeaturesList();
      },
      error: console.log,
    })
  }
  openEditForm(data: any){
    const dialogRef = this._dialog.open(AddEditFeatureComponent, {
      data, 
    });
    dialogRef.afterClosed().subscribe({
    next: (val) =>{
      if(val) {
        this.getFeaturesList();
      }
    }
   })
   }
}
