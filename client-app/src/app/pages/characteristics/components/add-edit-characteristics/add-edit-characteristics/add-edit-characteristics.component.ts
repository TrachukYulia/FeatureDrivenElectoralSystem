import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { CharacteristicsService } from '../../../../sevrices/characteristics.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CoreService } from '../../../../../core/components/core.service';


@Component({
  selector: 'app-add-edit-characteristics',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule, CommonModule, MatDialogModule, MatButtonModule,
    MatFormField, MatIcon, MatFormFieldModule, MatInputModule],
  templateUrl: './add-edit-characteristics.component.html',
  styleUrl: './add-edit-characteristics.component.css'
})
export class AddEditCharacteristicsComponent implements OnInit {
  charForm: FormGroup;
  constructor(private _fb: FormBuilder,
    private _charService: CharacteristicsService,
    private _dialogRef: MatDialogRef<AddEditCharacteristicsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService
  ) {
    this.charForm = this._fb.group({
      name: '',
    })
  }
  onFormSubmit() {
    if (this.charForm.valid) {
      if (this.data) {
        this._charService.updateCharacteristics(this.data.id, this.charForm.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Characteristic updated successfully!')

            this._dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
          },
        })
      }
      else {
        this._charService.addCharacteristics(this.charForm.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Characteristic added successfully!')
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
    this.charForm.patchValue(this.data);
  }
}
