import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AdminLayoutRoutes } from './admin-layout.routing';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductComponent } from 'app/pages/product/product.component';
import { TransactionComponent } from 'app/pages/transaction/transaction.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    NgbModule,
    NgxDatatableModule,
    ReactiveFormsModule
  ],
  declarations: [
    ProductComponent,
    TransactionComponent,
  ]
})

export class AdminLayoutModule {}
