import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { PrintingEditionComponent } from './printing-edition.component';
import { EditPrintingEditionComponent } from './edit-printing-edition/edit-printing-edition.component';


@NgModule({
  declarations: [PrintingEditionComponent, EditPrintingEditionComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([{
      path: '', children : [
      { path: '',component: PrintingEditionComponent},
      { path: 'editPrintingEdition', component: EditPrintingEditionComponent}
    ]
  }
])
],
  exports: [PrintingEditionComponent, EditPrintingEditionComponent],
})
export class PrintingEditionModule { }
