import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { TypeMessage } from "app/enums/TypeMessage";
import { MessageComponent } from "app/shared/notification/message.component";
import { ProductDto } from "app/usecase/dto/ProductDto";
import { TransactionDto } from "app/usecase/dto/TransactionDto";
import { Pager } from "app/usecase/entitie/Pager";
import { Product } from "app/usecase/entitie/Product";
import { Transaction } from "app/usecase/entitie/Transaction";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-register-modal",
  templateUrl: "./register.component.html",
})
export class RegisterModalComponent implements OnInit {
    private message: MessageComponent;
    public itemForm: FormGroup;

    public products: Pager<ProductDto>;
    
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<RegisterModalComponent>,
        private fb: FormBuilder,
        private toastr: ToastrService,
        private product: Product,
        private transact: Transaction
    ){
        this.message = new MessageComponent(this.toastr);
        this.products = new Pager<ProductDto>(0, false);
    }

    ngOnInit() {
        this.buildForm();
        if(this.data?.type == "buy"){
            this.getProducts("ACTIVO");
        }else{
            this.getProducts("ACTIVO", 0);
        }
    }

    buildForm(){
        this.itemForm = this.fb.group({
            productid: [0, Validators.required],
            price: [0, Validators.required],
            quantity: [0, Validators.required],
            note: ['']
        });
    }

    setTransact(){
        if (this.itemForm.invalid) {
            this.message.showNotification("bottom", "right", TypeMessage.Error, "Datos incorrectos")
            this.itemForm.markAllAsTouched();
            return;
        }else{
            this.savetTransact();
        }
    }

    savetTransact(){
        let item = new TransactionDto();
        item.ProductId = this.itemForm.value["productid"];
        item.TypeTransactionName = this.data?.type;
        item.Quantity = this.itemForm.value["quantity"];
        item.Price = this.itemForm.value["price"];
        item.Note = this.itemForm.value["note"];

        this.transact.insert(item).subscribe({
            next: (res) => {
                this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Datos guardados")
                this.dialogRef.close(this.itemForm.value);
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, "Error al guardar datos")
            }
        });
    }

    onKeyPressCodigo(event: KeyboardEvent) {
        const pattern = /^[A-Za-z0-9\-]$/; // letras, números, guión
        if (!pattern.test(event.key)) {
            event.preventDefault();
        }
    }

    onKeyPressName(event: KeyboardEvent) {
        const pattern = /^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\- ]$/;
        if (!pattern.test(event.key)) {
            event.preventDefault();
        }
    }

    onKeyPressOnlyText(event: KeyboardEvent) {
        const pattern = /^[A-Z-a-z]+$/;
        if (!pattern.test(event.key)) {
            event.preventDefault();
        }
    }

    getProducts(status: string, filterStockMin: number = null){
        let product = new ProductDto();
        product.Status = status;
        product.FilterStockMin = filterStockMin;
        product.FilterStockMax = null;
        product.FilterPriceMin = null;
        product.FilterPriceMax = null;

        this.product.getAll(product, false).subscribe({
            next: (res) => {
                this.products = res
            },
            error: (err) => {
                
            }
        });
    }
}