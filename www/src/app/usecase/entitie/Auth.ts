import { ServicesCrud } from "../../services/ServicesCrud"
import { Subject, takeUntil, tap } from "rxjs";
import { environment } from "environments/environment";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Auth {
    private UrlService: string;
    private unsuscribe$ = new Subject<void>();
    
    constructor(private crudService: ServicesCrud) {   
        this.UrlService = environment.serviceAuthUrl;
    }

    get(){
        return this.crudService.Post(
            {
            "user": environment.user,
            "pass": environment.pass
            },
            this.UrlService, `auth`
        ).pipe(
            takeUntil(this.unsuscribe$),
            tap((response: any) => {
                if (response) {
                    localStorage.setItem("token", response)
                }
            })
        );
    }
}