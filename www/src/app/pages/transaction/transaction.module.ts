import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterModalComponent } from './register/register.component';
@NgModule({
  declarations: [
    RegisterModalComponent,
    // otros componentes
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    // otros módulos
  ]
})
export class TransactiontModule { }
