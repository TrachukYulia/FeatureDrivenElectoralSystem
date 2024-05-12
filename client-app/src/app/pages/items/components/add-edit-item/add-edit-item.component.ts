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
import { Observable, combineLatest, map, startWith } from 'rxjs';
import { ItemFeatureRequest } from '../../../models/feature-item.model';
import { SelectedFeature } from '../../../models/selected-feature-model';
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
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _coreService: CoreService,
    private _itemService: ItemService,
    private _featureService: FeaturesService,
    private _charService: CharacteristicsService
  ) {
    this.itemForm = this._fb.group({
      name: ['', Validators.required]
    });
  }
  itemName!: string;
  selectedFeatures: { characteristicId: number, featureId: number }[] = [];
  characteristics: Characteristic[] = [];
  featureItems: ItemFeatureRequest[] = [];
  features: Features[] = []
  selectedFeatureIds: { [key: number]: number } = {};
  items: any[] = [];
  newItemName: string = '';
  displayedColumns: string[] = ['name'];
  formControls: Record<any, any> = {};

  addItem() {
    console.log(this.itemForm.value.featureItem.id)
    const newItem = {
      name: this.itemForm.value.name,
      featureItems: this.featureItems
    };
    for (const characteristic of this.characteristics) {
      const featureId2 = this.getFeatureId(characteristic.name)
      newItem.featureItems.push({
        featureId: 0
      });
    }
    this.itemForm.value.featureItem.id
    console.log(newItem)

    this._itemService.addItem(newItem).subscribe((data: any) => {
      console.log('Item added successfully:', data);
    });
  }
  
filterFeatures(characteristic: any, value: string): any[] {
  const filterValue = (value as string);
  return characteristic.features.filter((feature: any) => feature.name.includes(filterValue));
}
getSelectValue(event: Event): string {
  const value = (event.target as HTMLSelectElement)?.value;
  return value;
}

onFeatureSelect(featureId: string, characteristicName: string): void {
console.log(featureId)
}
  onFormSubmit() {
    console.log(this.itemForm)
    if (this.itemForm.valid) {
      console.log(this.itemForm.value.name)
      console.log(this.itemForm.value.featureItem)

      const newItem = {
        name: this.itemForm.value.name,
        featureItem: this.featureItems
      };
      for (const characteristic of this.characteristics) {
        // Находим запись в selectedFeatures, соответствующую текущей характеристике
        const selectedFeature = this.selectedFeatures.find(item => item.characteristicId === characteristic.id);
      
        // Если запись найдена, добавляем ее featureId в массив featureItem объекта newItem
        if (selectedFeature) {
          newItem.featureItem.push({
            featureId: selectedFeature.featureId
          });
        }
      }
      console.log(newItem)

      if (this.data) {
        this._itemService.updateItem(this.data.id, newItem).subscribe({
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
        this._itemService.addItem(newItem).subscribe({
          next: (val: any) => {
            this._coreService.openSnackBar('Item added successfully!')
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

    this.formControls['name'] = ['', Validators.required]; // Поле имени
    this.characteristics.forEach(characteristic => {
      this.formControls[characteristic.name] = ['', Validators.required]; // Используйте null, чтобы не предвыбрать значение
    });
    console.log(this.formControls);
    this.itemForm = this._fb.group(this.formControls);
    
    this.itemForm.valueChanges.subscribe((formValue: any) => {
      console.log(formValue)

  Object.keys(formValue).forEach(key => {
    const value = formValue[key];
    if (typeof value === 'object' && value !== null && 'id' in value) {
      const characteristicId = value.characteristicsId;
      const featureId = value.id;
      
      // Найдем индекс характеристики в массиве selectedFeatures
      const index = this.selectedFeatures.findIndex(item => item.characteristicId === characteristicId);
      if (index !== -1) {
        // Если характеристика уже существует в массиве selectedFeatures, обновим значение featureId
        this.selectedFeatures[index].featureId = featureId;
      } else {
        // Если характеристика еще не существует в массиве selectedFeatures, добавим новую запись
        this.selectedFeatures.push({ characteristicId, featureId });
      }
    }
  });

  console.log('characteristicId:', this.selectedFeatures);
    });
  
  }

  getfeaturesForCharacteristic2(name: string): Features[] {
    return this.features.filter(feature => feature.characteristicName === name);
  }
  getFeatureId(name: string): number[]{
    const arr = this.features.filter(feature => feature.characteristicName === name);
    return arr.map(f=> f.id);
  }
  getfeaturesForCharacteristic(characteristicId: number): Features[] {
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

    this.itemForm.patchValue(this.data);

  }
}
