import { Category } from "./category";

export class DataProduct {
    requestType!: number;
    public id!: number;
    public title!: string;
    public price!: number;
    public quantity!: number;
    public description!: string;
    public image!: string;
    public category!: Category;
}