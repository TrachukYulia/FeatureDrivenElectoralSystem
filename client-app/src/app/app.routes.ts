import { Routes } from '@angular/router';
import { CharacteristicsComponent } from './pages/characteristics/characteristics.component';
import { FeaturesComponent } from './pages/features/features.component';
import { ItemsComponent } from './pages/items/items.component';

export const routes: Routes = [
    {
        path: 'characteristics',
        component: CharacteristicsComponent
    },
    {
        path: 'features',
        component: FeaturesComponent
    },
    {
        path: 'items',
        component: ItemsComponent
    },
   
];
