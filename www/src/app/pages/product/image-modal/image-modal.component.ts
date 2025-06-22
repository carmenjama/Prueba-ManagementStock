import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Product } from "app/usecase/entitie/Product";

@Component({
  selector: "app-image-modal",
  templateUrl: "./image-modal.component.html",
})
export class ImageModalComponent implements OnInit {
    public imageData: string = "";
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<ImageModalComponent>,
        private product: Product
    ){
        
    }

    ngOnInit() {
        this.getImage(this.data.productId);
    }

    getImage(id: number){
        this.product.getImage(id).subscribe({
            next: (res) => {
                this.imageData = res;
            },
            error: (err) => {
                
            }
        });
    }
}