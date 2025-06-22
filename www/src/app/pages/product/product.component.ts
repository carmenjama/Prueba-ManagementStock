import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SelectionType } from '@swimlane/ngx-datatable';
import { TypeMessage } from 'app/enums/TypeMessage';
import { ServicesCrud } from 'app/services/ServicesCrud';
import { MessageComponent } from 'app/shared/notification/message.component';
import { CategoryDto } from 'app/usecase/dto/CategoryDto';
import { ProductDto } from 'app/usecase/dto/ProductDto';
import { Category } from 'app/usecase/entitie/Category';
import { Pager } from 'app/usecase/entitie/Pager';
import { Product } from 'app/usecase/entitie/Product';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'product-cmp',
    templateUrl: 'product.component.html',
    styleUrls: ["../../shared/styless/product.component.less"]
})

export class ProductComponent implements OnInit{
    public SelectionType = SelectionType;
    public itemForm: FormGroup;
    public products: Pager<ProductDto>;
    public categories: Pager<CategoryDto>;
    public status: any[] = [{id:"", name: "TODOS"}, {id:"ACTIVO", name: "ACTIVO"}, {id:"INACTIVO", name: "INACTIVO"}];
    private message: MessageComponent;
    constructor(
        private crudService: ServicesCrud,
        private fb: FormBuilder,
        private toastr: ToastrService,
        private dialog: MatDialog,
        
        private product: Product,
        private category: Category
    ) {
        this.products = new Pager<ProductDto>(10, true);
        this.categories = new Pager<CategoryDto>(10, true);

        this.product = new Product(this.crudService);
        this.category = new Category(this.crudService);
        this.message = new MessageComponent(this.toastr);
    }

    ngOnInit(){
        this.buildForm();
        this.getCategories();
        this.getProducts();
    }

    buildForm(){
        this.itemForm = this.fb.group({
            categoryid: [0, Validators.required],
            code: ['', Validators.required],
            name: ['', Validators.required],
            status: ['', Validators.required],
            minprice: [0, Validators.required],
            maxprice: [0, Validators.required],
            minstock: [0, Validators.required],
            maxstock: [0, Validators.required],
        });
    }

    getCategories(){
        this.category.getAll(false).subscribe({
            next: (res) => {
                this.categories = res;
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, "No se encontraron categorías")
            }
        });
    }

    getProducts(){
        let filters = new ProductDto();
        filters.CategoryId = this.itemForm.value["categoryid"];
        filters.Code = this.itemForm.value["code"];
        filters.Name = this.itemForm.value["name"];
        filters.Status = this.itemForm.value["status"];
        filters.FilterPriceMin = this.itemForm.value["minprice"];
        filters.FilterPriceMax = this.itemForm.value["maxprice"];
        filters.FilterStockMin = this.itemForm.value["minstock"];
        filters.FilterStockMax = this.itemForm.value["maxstock"];
        
        this.product.getAll(filters, true, this.products.pageNumber, this.products.limit).subscribe({
            next: (res) => {
                this.products.set(res);
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, "No se encontraron productos")
            }
        });
    }
    
    editProduct(item) {
        
    }

    deleteProduct(item) {
        this.product.delete(item.id)
        .subscribe({
            next: (res) => {
                this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Producto eliminado")
                this.getProducts();
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, "Error al eliminar producto")
            }
        });
    }

    activeProduct(item) {
        this.product.activar(item.id)
        .subscribe({
            next: (res) => {
                this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Producto activado")
                this.getProducts();
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, "Error al activar producto")
            }
        });
    }

    openFile(item) {
        let dialogRef: MatDialogRef<any> = this.dialog.open(
            PopupVehiculoOportunidadPagoComponent,
            {
                width: window.innerWidth > 1024 ? "75vh" : "100vh",
                disableClose: true,
                data: {
                title: title,
                payload: data,
                busqueda: this.busqueda,
                esCarrucel: true,
                },
            }
        );
        dialogRef.afterClosed().subscribe((res) => {
            if (!res) {
                // If user press cancel
                return;
            }
        });
    }
    
    setPage(event){
        this.products.pageNumber = event.offset + 1;
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

}
