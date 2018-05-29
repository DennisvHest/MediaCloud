import { Episode } from "./episode";

export class Season {
    id: number;
    seasonNumber: number;
    title: string;
    posterPath: string;

    episodes: Episode[];
}