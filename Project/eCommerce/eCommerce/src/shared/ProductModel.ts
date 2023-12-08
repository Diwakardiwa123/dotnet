export default class ProductModel {
    ProductID!: number;
    ProductName!: string;
    ProductDescription!: string | null;
    Price!: number;
    StockQuantity!: number;
    Category!: string | null;
    Manufacturer!: string | null;
    ImageURL!: string | null;
    IsNewCollection!: string | null;
}