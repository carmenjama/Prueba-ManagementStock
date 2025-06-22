import { Component, ElementRef, Inject, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { TypeMessage } from "app/enums/TypeMessage";
import { MessageComponent } from "app/shared/notification/message.component";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-register-modal",
  templateUrl: "./register.component.html",
})
export class RegisteModalComponent implements OnInit {
    private message: MessageComponent;
    
    public itemForm: FormGroup;
    @ViewChild('fileInput') fileInput: ElementRef<HTMLInputElement>;
    public imageSelect: string = "";
    
    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        public dialogRef: MatDialogRef<RegisteModalComponent>,
        private fb: FormBuilder,
        private toastr: ToastrService,
    ){
        this.message = new MessageComponent(this.toastr);
    }

    ngOnInit() {
        this.buildForm();
    }

    buildForm(){
        this.itemForm = this.fb.group({
            categoryid: [this.data?.product?.categoryId || 0, Validators.required],
            code: [{value : this.data?.product?.code || '', disabled: this.data?.type=='edit'}, Validators.required],
            name: [this.data?.product?.name || '', Validators.required],
            price: [this.data?.product?.price || 0, Validators.required],
            unit: [this.data?.product?.unit || '', Validators.required],
            note: [this.data?.product?.note || ''],
            multimedia: [''],
        });
        this.imageSelect = this.data?.product?.hasMultimedia ? 'Imagen' : '';
    }

    setProduct(){
        if (this.itemForm.invalid) {
            this.message.showNotification("bottom", "right", TypeMessage.Error, "Datos incorrectos")
            this.itemForm.markAllAsTouched();
            return;
        }else{
            this.dialogRef.close(this.itemForm.value);
        }
    }

    onFileInput() {
        this.fileInput.nativeElement.click();
    }

    onFileSelected(event: Event): void {
        const file = (event.target as HTMLInputElement).files?.[0];
        if (file) {
            this.imageSelect = file.name;
            const reader = new FileReader();
            reader.onload = () => {
                const base64String = reader.result as string;
                this.itemForm.value["multimedia"]=base64String;
            };
            reader.readAsDataURL(file);
        }else{
            this.imageSelect = "";
        }
    }

    deleteImage(){
        this.imageSelect = "";
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
}