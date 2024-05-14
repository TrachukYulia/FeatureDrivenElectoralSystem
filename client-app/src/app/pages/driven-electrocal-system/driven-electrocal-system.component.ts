import { Component } from '@angular/core';
import { GeneticAlgoComponent } from '../genetic-algo/genetic-algo.component';
import { GreedyAlgComponent } from '../greedy-alg/greedy-alg.component';

@Component({
  selector: 'app-driven-electrocal-system',
  standalone: true,
  imports: [GeneticAlgoComponent, GreedyAlgComponent],
  templateUrl: './driven-electrocal-system.component.html',
  styleUrl: './driven-electrocal-system.component.css'
})
export class DrivenElectrocalSystemComponent {

}
