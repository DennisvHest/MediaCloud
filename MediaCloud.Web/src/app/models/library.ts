import { Item } from "./item";

export class Library<T extends Item> {
    id: number;
    items: T[];
    name: string;
    libraryType: string;
}