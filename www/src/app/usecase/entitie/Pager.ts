export class Pager<T>{
    public isPager?: boolean;
    public pageNumber: number;
    public limit: number;
    public count: number;
    public total?: number;
    public elements: T[];

    constructor(cantidaPorPagina: number, paginadoServer: boolean) {
        this.clear();
        this.limit = cantidaPorPagina;
        this.isPager = paginadoServer;
    }

    clear() {
        this.count = 0;
        this.elements = [];
        this.pageNumber = 1;
        this.total = 0;
    }

    copy(objeto: Pager<T>){
        this.isPager = objeto.isPager;
        this.pageNumber = objeto.pageNumber;
        this.limit = objeto.limit;
        this.count = objeto.count;
        this.total = objeto.total;
        this.elements = objeto.elements;
    }

    set(data) {
        this.isPager = true;
        this.pageNumber = data.currentPage;
        this.limit = data.limit;
        this.count = data.numberPages;
        this.total = data.totalRecords;
        this.elements = data.elements;
    }
}