import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisteModalComponent } from './register/register.component';
import { CommonModule } from '@angular/common';
@NgModule({
  declarations: [
    RegisteModalComponent,
    // otros componentes
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    // otros módulos
  ]
})
export class ProductModule { }
