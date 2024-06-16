import { Routes } from '@angular/router';
import { CharacteristicsComponent } from './pages/characteristics/characteristics.component';
import { FeaturesComponent } from './pages/features/features.component';
import { ItemsComponent } from './pages/items/items.component';
import { GeneticAlgoComponent } from './pages/genetic-algo/genetic-algo.component';
import { DrivenElectrocalSystemComponent } from './pages/driven-electrocal-system/driven-electrocal-system.component';

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
    {
        path:'electrocaldriven',
        component: DrivenElectrocalSystemComponent
    },
    {
        path:'electrocalsystem',
        component: DrivenElectrocalSystemComponent
    },
    {
        path:'',
        component: DrivenElectrocalSystemComponent
    },
   
];
