import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeatureQueryService {
  private selectedFeatures: number[] = [];
  selectedFeaturesChanged = new BehaviorSubject<number[]>([]);

  setSelectedFeatures(features: number[]) {
    this.selectedFeatures = features;
    this.selectedFeaturesChanged.next(this.selectedFeatures);
  }

  getSelectedFeatures(): number[] {
    return this.selectedFeatures;
  }
  constructor() { }
}
