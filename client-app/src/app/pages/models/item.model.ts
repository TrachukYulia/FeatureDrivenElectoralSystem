import { ItemFeatureRequest } from "./feature-item.model";

export interface Item {
    // id: number,
    name: string,
    // selectedFeatures: { [key: number]: number };
    featureItem: ItemFeatureRequest[]; // Массив черт элемента
}
