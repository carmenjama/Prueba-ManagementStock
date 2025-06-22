export class ProductDto {
    Id: number = 0;
    CategoryId: number = 0;
    CategoryName: number = 0;
    Price: number = 0;
    Code: string = "";
    Name: string = "";
    Unit: string = "";
    Stock: string = "";
    Status: string = "";
    Note: string = "";
    HasMultimedia: boolean = false;

    FilterPriceMin: number = 0;
    FilterPriceMax: number = 0;
    FilterStockMin: number = 0;
    FilterStockMax: number = 0;
}