import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from '@angular/core';

@Injectable()
export class ServicesCrud {
    
    private HttpOptions;
    constructor(private httpClient: HttpClient) {
        this.HttpOptions = {headers: new HttpHeaders({ "Content-Type": "application/json" })};
    }

    Post(dto: any, url: string, api: string): Observable<any> {
        const body = JSON.stringify(dto);
        return this.httpClient.post<any[]>(url + api, body, this.HttpOptions);
    }

    Patch(dto: any, url: string, api: string): Observable<any> {
        var items = this.buildPatchDoc(dto);
        const body = JSON.stringify(items);
        return this.httpClient.patch<any>(`${url}${api}`, body, this.HttpOptions);
    }

    GetAll(filters: any, url: string, api: string): Observable<any> {
        let queryParams = new URLSearchParams();
        for (const [key, value] of Object.entries(filters)) {
            if (value !== null && value !== undefined && value !== '') {
                queryParams.append(key, value.toString());
            }
        }
        const fullUrl = `${url}${api}`;
        if(queryParams.size>0){
            const fullUrl = `&${queryParams.toString()}`;
        }
        return this.httpClient.get<any>(fullUrl, this.HttpOptions);
    }

    Delete(id: number, url: string, api: string): Observable<any> {
        return this.httpClient.delete<any>(`${url}${api}/${id}`, this.HttpOptions);
    }

    buildPatchDoc(dto: any): any[] {
        const patch: any[] = [];

        for (const [key, value] of Object.entries(dto)) {
            if (value !== undefined && value !== null) {
            patch.push({
                op: 'replace',
                path: `/${key}`,
                value: value
            });
            }
        }

        return patch;
    }
}