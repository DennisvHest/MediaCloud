import { Season } from './season';
import { Media } from './media';

export class Episode {
    id: number;
    episodeNumber: number;
    title: string;
    description: string;
    stillPath: string;
    season: Season;
    media: Media[];
}
