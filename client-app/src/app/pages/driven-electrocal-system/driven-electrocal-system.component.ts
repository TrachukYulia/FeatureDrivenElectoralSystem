import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { GeneticAlgoComponent } from '../genetic-algo/genetic-algo.component';
import { GreedyAlgComponent } from '../greedy-alg/greedy-alg.component';
import { Features } from '../models/features.model';
import { FeaturesService } from '../sevrices/features.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginator, MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatLineModule, MatPseudoCheckbox } from '@angular/material/core';
import {MatCheckboxChange, MatCheckboxModule} from '@angular/material/checkbox';
import {MatListModule, MatSelectionList} from '@angular/material/list';
import { SelectionModel } from '@angular/cdk/collections';
import { FeatureQueryService } from '../sevrices/feature-query.service';

@Component({
  selector: 'app-driven-electrocal-system',
  standalone: true,
  imports: [GeneticAlgoComponent, GreedyAlgComponent, RouterLink, FormsModule, MatDialogModule,
    MatButtonModule, MatFormField, MatIcon, ReactiveFormsModule, MatTableModule,
    MatPaginator, MatPaginatorModule, MatFormFieldModule, CommonModule, MatCheckboxModule, MatListModule  ],
  templateUrl: './driven-electrocal-system.component.html',
  styleUrl: './driven-electrocal-system.component.css'
})
export class DrivenElectrocalSystemComponent implements OnInit {
  @ViewChild('listOfSelectedFeachures') listOfSelectedFeachures!: MatSelectionList;
  features: any = {}; 
  selectedFeatures: number[] = [];
  selectedFeatureIds: number[] = [];
  selectedOptions: any[] = [];
  queryString!: string;

  constructor(
    private _featureService: FeaturesService,
    private _featureQueryService: FeatureQueryService
  )
  
  {}
  ngOnInit(): void {
    this.loadFeatures();
    
  }
  loadSelectAllOption()
  {
    this.listOfSelectedFeachures.options.forEach( option=>{
      option.selected = true;
    })
    this.features.forEach((feature: any) => {
      if (!this.selectedOptions.includes(feature)) {
        this.selectedOptions.push(feature);
      }
    });
  }
  loadFeatures() {
    this._featureService.getFeatures().subscribe({
      next: (res) => {
        this.features = res;
        this.loadSelectAllOption();
        console.log('Features is load:', this.features)
      },
      error: console.log,
    })
  }
  onSubmit()
  {
    console.log('Selected features:', this.selectedOptions);
    this.getIdOfSelectedFeachers();
  }
  getIdOfSelectedFeachers()
  {
    this.selectedFeatureIds = this.selectedOptions.map(x=>x.id);
    console.log('Selected feature IDs:', this.selectedFeatureIds);
    this.queryString =  this.selectedFeatureIds.map(id => `id=${id}`).join('&');
    this._featureQueryService.setSelectedFeatures(this.selectedFeatureIds);

    console.log('queryParams:', this.queryString);
  }
toggleSelection(feature: any) {
  const index = this.selectedOptions.indexOf(feature);
  if (index === -1) {
    this.selectedOptions.push(feature);
  } else {
    this.selectedOptions.splice(index, 1);
  }
}
  // getSelectedFeatureIds(): number[] {
  //   this.listOfSelectedFeachures.selectedOptions.selected.forEach(option => {
  //     if (option.value && option.value.id !== undefined) {
  //       this.selectedFeatureIds.push(option.value.id);
  //     } else {
  //       console.error("Invalid option:", option);
  //     }
  //   });
  //   this.listOfSelectedFeachures.selectedOptions.deselect.forEach(option => {
  //     if (option.value && option.value.id !== undefined) {
  //       this.selectedFeatureIds.push(option.value.id);
  //     } else {
  //       console.error("Invalid option:", option);
  //     }
  //   });
  //   console.log("Selected feature IDs:", this.selectedFeatureIds);
  //   return this.selectedFeatureIds;
  // }
  
 
  selectAllOptions(event: MatCheckboxChange) {
    if (event.checked) {
      this.listOfSelectedFeachures.options.forEach(option => {
        option.selected = true;
      });
      this.features.forEach((feature: any) => {
        if (!this.selectedOptions.includes(feature)) {
          this.selectedOptions.push(feature);
        }
      });
    } else {
      this.listOfSelectedFeachures.options.forEach(option => {
        option.selected = false;
      });
      this.selectedOptions = [];
    }
  }
}
