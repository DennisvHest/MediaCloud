import { Episode } from './episode';
import { Item } from './item';
import { Library } from './library';

export class Media {
    id: number;
    episode: Episode;
    movie: Item;
    library: Library<Item>;
}
