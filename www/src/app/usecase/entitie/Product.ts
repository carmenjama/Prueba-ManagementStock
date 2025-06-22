import { ServicesCrud } from "../../services/ServicesCrud"
import { Observable, Subject, takeUntil } from "rxjs";
import { ProductDto } from "../dto/ProductDto";
import { environment } from "environments/environment";
import { Pager } from "./Pager";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class Product {
    private UrlService: string;
    private unsuscribe$ = new Subject<void>();
    
    constructor(private crudService: ServicesCrud) {   
        this.UrlService = environment.serviceProductUrl;
    }

    insert(categoryId: number, price: number, code: string, 
        name: string, unit: string, note: string) {        
        this.crudService.Post(
            {
                categoryId, price, code, name, note
            }, 
            this.UrlService, "product"
        ).pipe(takeUntil(this.unsuscribe$))
        .subscribe((res) => {
            console.log("Aqui guardo datos", res)
            return res
        },(error) =>{
            console.log("Aqui guardo datos erros", error)
            return error
        });
    }

    activar(id: number):Observable<Pager<ProductDto>>{
        return this.crudService.Patch({status: 'ACTIVO'}, 
            this.UrlService, `product/${id}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    getAll(dto: ProductDto, isPaginated: boolean, page: number = 0, limit: number = 0):Observable<Pager<ProductDto>>{
        return this.crudService.GetAll(dto,
            this.UrlService,
            `product/${isPaginated}?page=${page}&limit=${limit}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    delete(id: number = 0):Observable<Pager<ProductDto>>{
        return this.crudService.Delete(id, this.UrlService, `product`).pipe(takeUntil(this.unsuscribe$));
    }
}