import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { PrintingEditionComponent } from './printing-edition.component';
import { CreatePrintingEditionComponent } from './create-printing-edition/create-printing-edition.component';
import { EditPrintingEditionComponent } from './edit-printing-edition/edit-printing-edition.component';

@NgModule({
  imports: [RouterModule.forChild([{
    path: '', children : [
        { path: '', component: PrintingEditionComponent},
        { path: 'printingEdition/create', component: CreatePrintingEditionComponent},
        { path: 'printingEdition/edit', component: EditPrintingEditionComponent}
      ]
    }
  ])
  ],
  exports: [RouterModule],
})

export class PrintingEditionRoutingModule { }
