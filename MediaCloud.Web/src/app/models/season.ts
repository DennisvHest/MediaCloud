import { Episode } from "./episode";
import { Item } from "./item";
import { AppSettings } from "../../AppSettings";

export class Season {
    id: number;
    seasonNumber: number;
    title: string;
    posterPath: string;

    series: Item;
    episodes: Episode[];
    
    posterUrl(size: string): string {
        return AppSettings.imageUrl(this.posterPath != null ? this.posterPath : this.posterPath, size);
    }
}