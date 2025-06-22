import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
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
import { ImageModalComponent } from './image-modal/image-modal.component';
import { RegisteModalComponent } from './register/register.component';

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
            minprice: [null, Validators.required],
            maxprice: [null, Validators.required],
            minstock: [null, Validators.required],
            maxstock: [null, Validators.required],
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
        
        this.product.getAll(filters, true, 1, 10).subscribe({
            next: (res) => {
                this.products.set(res);
            },
            error: (err) => {
                this.message.showNotification("bottom", "right", TypeMessage.Error, "No se encontraron productos")
            }
        });
    }
    
    editProduct(item) {
        let dialogRef: MatDialogRef<any> = this.dialog.open(
            RegisteModalComponent,
            {
                width: window.innerWidth > 1024 ? "75vh" : "100vh",
                disableClose: true,
                data: {
                    title: "Editar producto",
                    product: item,
                    categories: this.categories,
                    type:  "edit"
                },
            }
        );
        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.product.update(res, item.id)
                .subscribe({
                    next: (res) => {
                        this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Producto actualizado")
                        this.getProducts();
                    },
                    error: (err) => {
                        this.message.showNotification("bottom", "right", TypeMessage.Error, "Error al actualizar producto")
                    }
                }); 
            }
        });
    }

    addProduct() {
        let dialogRef: MatDialogRef<any> = this.dialog.open(
            RegisteModalComponent,
            {
                width: window.innerWidth > 1024 ? "75vh" : "100vh",
                disableClose: true,
                data: {
                    title: "Agregar producto",
                    categories: this.categories,
                    type:  "add"
                },
            }
        );
        dialogRef.afterClosed().subscribe((res) => {
            if (res) {
                this.product.insert(res)
                .subscribe({
                    next: (res) => {
                        this.message.showNotification("bottom", "right", TypeMessage.Sucess, "Producto agregado")
                        this.getProducts();
                    },
                    error: (err) => {
                        this.message.showNotification("bottom", "right", TypeMessage.Error, "Error al agregar producto")
                    }
                }); 
            }
        });
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
        this.product.active(item.id)
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
        if(item.hasMultimedia){
            let dialogRef: MatDialogRef<any> = this.dialog.open(
                ImageModalComponent,
                {
                    width: window.innerWidth > 1024 ? "75vh" : "100vh",
                    disableClose: true,
                    data: {
                        title: "Imagen producto",
                        productId: item.id
                    },
                }
            );
        }else{
            this.message.showNotification("bottom", "right", TypeMessage.Info, "Producto no tiene imagen adjunta")
        }
    }
    
    setPage(event){
        this.products.pageNumber = event.offset + 1;
        this.products.limit = 10;
        this.getProducts();
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
