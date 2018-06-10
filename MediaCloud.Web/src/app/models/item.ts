import { Season } from './season';
import { Genre } from './genre';
import { AppSettings } from '../../AppSettings';

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

    posterUrl(size: string): string {
        return AppSettings.imageUrl(this.posterPath, size);
    }
}
