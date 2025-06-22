import { ServicesCrud } from "../../services/ServicesCrud"
import { environment } from "../../../environments/environment";
import { Injectable } from "@angular/core";
import { Observable, Subject, takeUntil } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TypeTransaction {
    private UrlService: string;
    private unsuscribe$ = new Subject<void>();
    
    constructor(private crudService: ServicesCrud) {   
        this.UrlService = environment.serviceTransUrl;
    }

    getAll(isPaginated: boolean, page: number = 0, limit: number = 0):Observable<any>{
        return this.crudService.GetAll({status: "ACTIVO"},
            this.UrlService,
            `type-transaction/${isPaginated}?page=${page}&limit=${limit}`
        ).pipe(takeUntil(this.unsuscribe$));
    }

}
