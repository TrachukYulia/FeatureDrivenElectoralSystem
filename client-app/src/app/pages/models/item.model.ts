import { FeatureItem } from "./item-feature.model";

export interface Item {
    // id: number,

    nameOfItem: string,
    // selectedFeatures: { [key: number]: number };
    featureItem?: FeatureItem[]; // Массив черт элемента
    
    
}
