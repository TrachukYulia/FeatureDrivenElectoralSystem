import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
// import { MatDialog } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CoreService } from '../../../../core/components/core.service';
import {MatSelectModule} from '@angular/material/select';
import { Characteristic } from '../../../models/characteristic.model';
import { ItemService } from '../../../sevrices/item.service';
@Component({
  selector: 'app-add-edit-item',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule, CommonModule, MatDialogModule, MatButtonModule,
    MatFormField, MatIcon, MatFormFieldModule, MatInputModule, MatSelectModule],
  templateUrl: './add-edit-item.component.html',
  styleUrl: './add-edit-item.component.css'
})
export class AddEditItemComponent implements OnInit {
  itemForm: FormGroup;
    constructor(private _fb: FormBuilder,
      private _dialogRef: MatDialogRef<AddEditItemComponent>,
      @Inject(MAT_DIALOG_DATA) public featureData: any,
      private _coreService: CoreService, 
      private _itemService: ItemService
    ) {
      this.itemForm = this._fb.group({ 
        name: '',
      })
    }
    onFormSubmit() {
      if (this.itemForm.valid) {
        if (this.featureData) {
          this._itemService.updateItem(this.featureData.id, this.itemForm.value).subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('Feature updated successfully!')
              console.log(this.itemForm.value);
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          })
        }
        else {
          this._itemService.addItem(this.itemForm.value).subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('Feature added successfully!')
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          })
        }
      }
    }
  
    ngOnInit(): void {
      this.itemForm.patchValue(this.featureData);
    }
   
}
