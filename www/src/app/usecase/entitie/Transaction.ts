import { ServicesCrud } from "../../services/ServicesCrud"
import { Observable, Subject, takeUntil } from "rxjs";
import { ProductDto } from "../dto/ProductDto";
import { environment } from "environments/environment";
import { Pager } from "./Pager";
import { Injectable } from "@angular/core";
import { TransactionDto } from "../dto/TransactionDto";

@Injectable({
  providedIn: 'root'
})
export class Transaction {
    private UrlService: string;
    private unsuscribe$ = new Subject<void>();
    
    constructor(private crudService: ServicesCrud) {   
        this.UrlService = environment.serviceProductUrl;
    }

    insert(dto: TransactionDto):Observable<any>{        
        return this.crudService.Post(dto, this.UrlService, "transaction"
        ).pipe(takeUntil(this.unsuscribe$));
    }

    // update(dto: ProductDto, id: number):Observable<any>{
    //     return this.crudService.Patch(dto, 
    //         this.UrlService, `product/${id}`
    //     ).pipe(takeUntil(this.unsuscribe$));
    // }

    getAll(dto: TransactionDto, isPaginated: boolean, page: number = 0, limit: number = 0):Observable<any>{
        return this.crudService.GetAll(dto,
            this.UrlService,
            `transaction/${isPaginated}?page=${page}&limit=${limit}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

    // delete(id: number = 0):Observable<any>{
    //     return this.crudService.Delete(id, this.UrlService, `product`).pipe(takeUntil(this.unsuscribe$));
    // }
}