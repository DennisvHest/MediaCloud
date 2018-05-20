import { Season } from "./season";

export abstract class Item {
    id: number;
    title: string;
    posterPath: string;
    seasons: Season[];
    itemType: string;
}