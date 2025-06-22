export class TransactionDto {
    Id: number = 0;
    TypeTransactionId: number = 0;
    ProductId: number = 0;
    ProductName: string = "";
    TypeTransactionName: string = "";
    Note: string = "";
    Status: string = "";
    Quantity: number = 0;
    Price: number = 0;
    TransactionDate: Date;
    FilterMinDate: Date;
    FilterMaxDate: Date;
}