import { Item } from "./item";

export class Library<T extends Item> {
    items: Item[];
    name: string;
}