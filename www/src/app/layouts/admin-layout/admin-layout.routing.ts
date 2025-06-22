import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import * as path from 'path';
import { TransactionComponent } from 'app/pages/transaction/transaction.component';
import { ProductComponent } from 'app/pages/product/product.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'product',        component: ProductComponent },
    { path: 'transaction',    component: TransactionComponent }
];
