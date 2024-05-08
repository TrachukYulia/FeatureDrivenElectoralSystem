import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
// import { MatDialog } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Subscription, combineLatest } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CoreService } from '../../../../core/components/core.service';
import { FeaturesService } from '../../../sevrices/features.service';
import {MatSelectModule} from '@angular/material/select';
import { Characteristic } from '../../../models/characteristic.model';
import { CharacteristicsService } from '../../../sevrices/characteristics.service';
import { Features } from '../../../models/features.model';
import { addFeature } from '../../../models/add-feature';

@Component({
  selector: 'app-add-edit-feature',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule, CommonModule, MatDialogModule, MatButtonModule,
    MatFormField, MatIcon, MatFormFieldModule, MatInputModule, MatSelectModule],
  templateUrl: './add-edit-feature.component.html',
  styleUrl: './add-edit-feature.component.css'
})
export class AddEditFeatureComponent implements OnInit{
 
  //  feature: addFeature = { id: 0, name: '', characteristicId: 0 };
  
  selectedCharacteristicId!: FormControl;
  characteristics: Characteristic[] = [];
  featureForm: FormGroup;
    constructor(private _fb: FormBuilder,
      private _featureService: FeaturesService,
      private _dialogRef: MatDialogRef<AddEditFeatureComponent>,
      @Inject(MAT_DIALOG_DATA) public featureData: any,
      private _coreService: CoreService, 
      private characteristicService: CharacteristicsService
    ) {
      this.featureForm = this._fb.group({
  
        featureName: '',
        name: '',
        CharacteristicName: ''
  
      })
    }
    onFormSubmit() {
      if (this.featureForm.valid) {
        const formValue = this.featureForm.value;
        formValue.characteristicId = this.selectedCharacteristicId;
        if (this.featureData) {
          // this.feature.characteristicId = this.selectedCharacteristicId;
          this._featureService.updateFeature(this.featureData.id, formValue).subscribe({
            next: (val: any) => {
              this._coreService.openSnackBar('Feature updated successfully!')
              console.log(this.featureForm.value);
              this._dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
            },
          })
        }
        else {
          this._featureService.addFeature(this.featureForm.value).subscribe({
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
    loadCharacteristics() {
      this.characteristicService.getCharacteristics().subscribe(characteristics => {
        this.characteristics = characteristics;
      });
    }
    ngOnInit(): void {
      this.featureForm = this._fb.group({
        featureName: ['', Validators.required], // Устанавливаем валидатор обязательного поля
        characteristicId: [null, Validators.required], // Устанавливаем валидатор обязательного поля
      });
      this.featureForm.patchValue(this.featureData);
      this.loadCharacteristics();
    }
    onCharacteristicChange() {
      // Обновляем выбранную характеристику
      console.log(this.selectedCharacteristicId);
    }
  }
  