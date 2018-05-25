import { Season } from "./season";
import { Genre } from "./Genre";

export abstract class Item {
    id: number;
    title: string;
    description: string;
    releaseDate: Date;
    posterPath: string;
    backdropPath: string;
    genres: Genre[];
    seasons: Season[];
    itemType: string;
}