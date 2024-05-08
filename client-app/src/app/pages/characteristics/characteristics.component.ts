import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CharacteristicsService } from '../sevrices/characteristics.service';
import { AddEditCharacteristicsComponent } from './components/add-edit-characteristics/add-edit-characteristics/add-edit-characteristics.component';
import { MatIcon } from '@angular/material/icon';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { CoreService } from '../../core/components/core.service';

@Component({
  selector: 'app-characteristics',
  standalone: true,
  imports: [RouterLink, FormsModule, MatDialogModule,
     MatButtonModule, MatFormField, MatIcon, ReactiveFormsModule, MatTableModule,
     MatPaginator, MatPaginatorModule, MatFormFieldModule, MatSnackBarModule

  ],
  templateUrl: './characteristics.component.html',
  styleUrl: './characteristics.component.css'
})
export class CharacteristicsComponent implements OnInit{

  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private _dialog: MatDialog,
    private _charService: CharacteristicsService,
    private _coreService: CoreService
  
  ){}
  ngOnInit(): void {
    this.getCharacteristicsList();
  }
  openAddEditCharForm(){
   const dialogRef = this._dialog.open(AddEditCharacteristicsComponent);
   dialogRef.afterClosed().subscribe({
    next: (val) =>{
      if(val) {
        this.getCharacteristicsList();
      }
    }
   })
  }
  // ngOnInit(): void {
  //   throw new Error('Method not implemented.');
  // }
  getCharacteristicsList(){
    this._charService.getCharacteristics().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: console.log,
    })
  }
  deleteCharacteristics(id: number)
  {
    this._charService.deleteCharacteristics(id).subscribe({
      next: (res) => {
       this._coreService.openSnackBar('Characteristic deleted!')
       this.getCharacteristicsList();
      },
      error: console.log,
    })
  }
  openEditForm(data: any){

    const dialogRef = this._dialog.open(AddEditCharacteristicsComponent, {
      data, 
    });
    dialogRef.afterClosed().subscribe({
    next: (val) =>{
      if(val) {
        this.getCharacteristicsList();
      }
    }
   })
   }
}
