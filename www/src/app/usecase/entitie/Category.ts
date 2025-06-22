import { ServicesCrud } from "../../services/ServicesCrud"
import { Observable, Subject, takeUntil } from "rxjs";
import { environment } from "environments/environment";
import { Pager } from "./Pager";
import { CategoryDto } from "../dto/CategoryDto";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Category {
    private UrlService: string;
    private unsuscribe$ = new Subject<void>();
    
    constructor(private crudService: ServicesCrud) {   
        this.UrlService = environment.serviceProductUrl;
    }

    getAll(isPaginated: boolean, page: number = 0, limit: number = 0):Observable<Pager<CategoryDto>>{
        return this.crudService.GetAll({},
            this.UrlService,
             `category/${isPaginated}?page=${page}&limit=${limit}`
        ).pipe(takeUntil(this.unsuscribe$));
    }
}