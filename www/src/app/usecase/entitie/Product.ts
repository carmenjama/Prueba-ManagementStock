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

    insert(dto: ProductDto):Observable<any>{        
        return this.crudService.Post(dto, this.UrlService, "product"
        ).pipe(takeUntil(this.unsuscribe$));
    }

    active(id: number):Observable<any>{
        return this.crudService.Patch({status: 'ACTIVO'}, 
            this.UrlService, `product/${id}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    update(dto: ProductDto, id: number):Observable<any>{
        return this.crudService.Patch(dto, 
            this.UrlService, `product/${id}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    getAll(dto: ProductDto, isPaginated: boolean, page: number = 0, limit: number = 0):Observable<Pager<ProductDto>>{
        return this.crudService.GetAll(dto,
            this.UrlService,
            `product/${isPaginated}?page=${page}&limit=${limit}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    getImage(id: number):Observable<any>{
        return this.crudService.GetAll({id: id},
            this.UrlService, `image/${id}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    delete(id: number = 0):Observable<any>{
        return this.crudService.Delete(id, this.UrlService, `product`).pipe(takeUntil(this.unsuscribe$));
    }
}