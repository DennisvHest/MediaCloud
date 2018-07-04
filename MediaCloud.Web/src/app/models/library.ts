import { Item } from './item';
import { Media } from './media';

export class Library<T extends Item> {
    id: number;
    items: T[];
    name: string;
    media: Media[];
    libraryType: string;
}
