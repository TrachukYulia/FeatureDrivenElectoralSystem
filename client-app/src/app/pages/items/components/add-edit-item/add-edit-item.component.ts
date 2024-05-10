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
import { MatSelectModule } from '@angular/material/select';
import { Characteristic } from '../../../models/characteristic.model';
import { ItemService } from '../../../sevrices/item.service';
import { Item } from '../../../models/item.model';
import { FeaturesService } from '../../../sevrices/features.service';
import { CharacteristicsService } from '../../../sevrices/characteristics.service';
import { Features } from '../../../models/features.model';
import { RouterFeatures } from '@angular/router';
import { combineLatest } from 'rxjs';
@Component({
  selector: 'app-add-edit-item',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule, CommonModule, MatDialogModule, MatButtonModule,
    MatFormField, MatIcon, MatFormFieldModule, MatInputModule, MatSelectModule],
  templateUrl: './add-edit-item.component.html',
  styleUrl: './add-edit-item.component.css'
})
export class AddEditItemComponent implements OnInit {

  itemForm!: FormGroup;

  constructor(private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<AddEditItemComponent>,
    @Inject(MAT_DIALOG_DATA) public itemData: any,
    private _coreService: CoreService,
    private _itemService: ItemService,
    private _featureService: FeaturesService,
    private _charService: CharacteristicsService

  ) {
    // this.itemForm = this._fb.group({
    //   id: Number,
    //   name: '',
    //   characteristics: this.getCharacteristicsList();
    // })
    this.itemForm = this._fb.group({
      name: ['', Validators.required]
    });
  }
  itemName!: string;
  selectedFeatures: { characteristicId: number, featureId: number }[] = [];
  characteristics: Characteristic[] = [];
  features: Features[] = []
  // newItem: Item = { nameOfItem: '', featureItem: [] };
  selectedFeatureIds: { [key: number]: number } = {};

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
  // onFormSubmit() {
  //   if (this.itemForm.valid) {
  //     if (this.itemData) {
  //       const formValue = this.itemForm.value;
  //       const itemFeatures: any[] = [];
  //       this.characteristics.forEach(characteristic => {
  //         const featureId = formValue[characteristic.id];
  //         if (featureId) {
  //           const itemFeature = {
  //             characteristicId: characteristic.id,
  //             featureId: featureId
  //           };
  //           console.log('submit');
  //           console.log(itemFeature);
  //           itemFeatures.push(itemFeature);
  //         }
  //       });
  //     }
  //     else {
  //       this._itemService.addItem(this.itemForm.value).subscribe({
  //         next: (val: any) => {
  //           this._coreService.openSnackBar('Feature added successfully!')
  //           // this.newItem = { name: '', featureItem: [] };
  //           // this.selectedFeatures = [];

  //           this._dialogRef.close(true);
  //         },
  //         error: (err: any) => {
  //           console.error(err);
  //         },
  //       })
  //     }
  //   }
  // }
  onFormSubmit() {
    if (this.itemForm.valid) {
      if (this.itemData) {
        this._itemService.updateItem(this.itemData.id, this.itemForm.value).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Item updated successfully!')
            console.log(val);
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
            this._coreService.openSnackBar('Characteristic added successfully!')
            console.log(val);
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
    this._charService.getCharacteristics().subscribe(characteristics => {
      this.characteristics = characteristics;
      this.createForm();
    });
  }
  loadFeatures() {
    this._featureService.getFeatures().subscribe(f => {
      this.features = f;
      this.createForm();
    });
  }
  createForm() {

    const formControls: Record<any, any> = {};
    formControls['nameOfItem'] = ['', Validators.required]; // Поле имени
    this.characteristics.forEach(characteristic => {
      formControls[characteristic.name] = ['', Validators.required]; // Используйте null, чтобы не предвыбрать значение
    });


    console.log(formControls);
    this.itemForm = this._fb.group(formControls);
  }

  featuresForCharacteristic2(name: string): Features[] {
    return this.features.filter(feature => feature.characteristicName === name);
  }
  featuresForCharacteristic(characteristicId: number): Features[] {
    return this.features.filter(feature => feature.characteristicId === characteristicId);
  }

  selectedCharacteristicControl = new FormControl('', Validators.required);

  // Викликається при зміні вибраної характеристики
  onCharacteristicChange(event: any) {
    console.log('Вибрана характеристика:', this.selectedCharacteristicControl.value);
  }

  ngOnInit(): void {
    this.loadCharacteristics();
    this.loadFeatures();
    this.createForm();

    this.itemForm.patchValue(this.itemData);

  }
}
