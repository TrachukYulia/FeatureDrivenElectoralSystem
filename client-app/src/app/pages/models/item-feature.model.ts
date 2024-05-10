import { Features } from "./features.model";

export interface FeatureItem {
  itemFeatureId?: number; // Опциональный идентификатор черты элемента, так как он будет генерироваться на сервере
  itemId?: number; // Опциональный идентификатор элемента, с которым связана черта
  featureId: number; // Идентификатор черты (Feature)
  feature?: Features; // Ссылка на объект черты (Feature)
}
