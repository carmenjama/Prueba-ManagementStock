import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SelectionType } from '@swimlane/ngx-datatable';
import { TypeMessage } from 'app/enums/TypeMessage';
import { ServicesCrud } from 'app/services/ServicesCrud';
import { MessageComponent } from 'app/shared/notification/message.component';
import { TransactionDto } from 'app/usecase/dto/TransactionDto';
import { TypeTransactionDto } from 'app/usecase/dto/TypeTransactionDto';
import { Pager } from 'app/usecase/entitie/Pager';
import { Transaction } from 'app/usecase/entitie/Transaction';
import { TypeTransaction } from 'app/usecase/entitie/TypeTransaction';
import { ToastrService } from 'ngx-toastr';
import { RegisterModalComponent } from './register/register.component';
declare interface TableData {
    headerRow: string[];
    dataRows: string[][];
}

@Component({
    selector: 'product-cmp',
    templateUrl: 'transaction.component.html',
    styleUrls: ["../../shared/styless/product.component.less"]
})

export class TransactionComponent implements OnInit{
    public SelectionType = SelectionType;

    public transacts: Pager<TypeTransactionDto>;
    public status: any[] = [{id:"", name: "TODOS"}, {id:"ACTIVO", name: "ACTIVO"}, {id:"INACTIVO", name: "INACTIVO"}];
    public items: Pager<TransactionDto>;
    
    private message: MessageComponent;
    constructor(private fb: FormBuilder,
        private toastr: ToastrService,
        private dialog: MatDialog,
        private crudService: ServicesCrud,
        private type: TypeTransaction,
        private transaction: Transaction,
    ) {
        
        this.message = new MessageComponent(this.toastr);
        this.transaction = new Transaction(this.crudService);

        this.transacts = new Pager<TypeTransactionDto>(10, true);
        this.items = new Pager<TransactionDto>(10, true);
    }

    public itemForm: FormGroup;
    
    ngOnInit(){
        this.getTransactions();
        this.buildForm();
        this.getItems();
    }

    buildForm(){
        this.itemForm = this.fb.group({
            productname: ['', Validators.required],
            transactiontypeid: [0, Validators.required],
            status: ['', Validators.required],
            mindate: [null, Validators.required],
            maxdate: [null, Validators.required],
        });
    }

    getTransactions(){
        this.type.getAll(false).subscribe({
            next: (res) => {
                this.transacts =res;
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, err?.error?.message || "No se encontraron tipos")
            }
        });
    }

    getItems(){
        let filters = new TransactionDto();
        filters.TypeTransactionId = this.itemForm.value["transactiontypeid"];
        filters.ProductName = this.itemForm.value["productname"];
        filters.Status = this.itemForm.value["status"];
        filters.FilterMinDate = this.itemForm.value["mindate"];
        filters.FilterMaxDate = this.itemForm.value["maxdate"];

        this.transaction.getAll(filters, true, 1, 10).subscribe({
            next: (res) => {
                this.items.set(res);
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, err?.error?.message || "No se encontraron transacciones")
            }
        });
    }

    buy(){
        let dialogRef: MatDialogRef<any> = this.dialog.open(
            RegisterModalComponent,
            {
                width: window.innerWidth > 1024 ? "75vh" : "100vh",
                disableClose: true,
                data: {
                    title: "Registrar compra",
                    type: "COMPRA"
                },
            }
        );
        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Compra registrada")
                this.getItems();
            }
        });
    }

    sale(){
        let dialogRef: MatDialogRef<any> = this.dialog.open(
            RegisterModalComponent,
            {
                width: window.innerWidth > 1024 ? "75vh" : "100vh",
                disableClose: true,
                data: {
                    title: "Registrar venta",
                    type: "VENTA"
                },
            }
        );
        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Venta registrada")
                this.getItems();
            }
        });
    }

    setPage(event){
        this.items.pageNumber = event.offset + 1;
        this.items.limit = 10;
        this.getItems();
    }

    onKeyPressName(event: KeyboardEvent) {
        const pattern = /^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\- ]$/;
        if (!pattern.test(event.key)) {
            event.preventDefault();
        }
    }

     delete(item) {
        this.transaction.delete(item.id)
        .subscribe({
            next: (res) => {
                this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Transacción eliminado")
                this.getItems();
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, err?.error?.message || "Error al eliminar transacción")
            }
        });
    }
}
