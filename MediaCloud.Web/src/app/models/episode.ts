import { Season } from './season';

export class Episode {
    id: number;
    episodeNumber: number;
    title: string;
    description: string;
    stillPath: string;
    season: Season;
}
