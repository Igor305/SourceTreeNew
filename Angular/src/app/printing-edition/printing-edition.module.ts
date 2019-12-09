import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule} from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PrintingEditionComponent } from './printing-edition.component';
import { CreatePrintingEditionComponent } from './create-printing-edition/create-printing-edition.component';
import { EditPrintingEditionComponent } from './edit-printing-edition/edit-printing-edition.component';
import { PrintingEditionRoutingModule } from './printing-edition-routing.module';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  declarations: [PrintingEditionComponent, CreatePrintingEditionComponent, EditPrintingEditionComponent],
  imports: [
    CommonModule,
    FormsModule, 
    ReactiveFormsModule,
    MatSelectModule,
    MatNativeDateModule,
    PrintingEditionRoutingModule
  ],
  exports: [PrintingEditionComponent, EditPrintingEditionComponent, CreatePrintingEditionComponent],
})
export class PrintingEditionModule { }
