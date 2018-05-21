import { Season } from "./season";

export abstract class Item {
    id: number;
    title: string;
    posterPath: string;
    backdropPath: string;
    seasons: Season[];
    itemType: string;
}