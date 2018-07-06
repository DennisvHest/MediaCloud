import { Item } from '../item';
import { Season } from '../season';
import { Episode } from '../episode';
import { Media } from '../media';

export class ItemCardModel {
    posterPath: string;
    stillPath: string;
    backupPosterPath: string;
    backupStillPath: string;
    imageWidth: string;
    title: string;
    linkPath: string[];
    badges: BadgeModel[];
    cardType: string;

    constructor(item?: Item, season?: Season, episode?: Episode, media?: Media) {
        this.imageWidth = 'w342';

        if (item) {
            this.cardType = 'item';
            this.posterPath = item.posterPath;

            this.title = item.title;
            this.linkPath = ['/items', item.id.toString()];

            if (item.itemType === 'Series') {
                this.badges = [new BadgeModel(`${item.seasonCount} Seasons`, null)];
            }
        }

        if (season) {
            this.cardType = 'season';
            this.posterPath = season.posterPath;
            this.backupPosterPath = season.series.posterPath;
            this.title = season.title;
            this.linkPath = ['/items', 'seasons',  season.id.toString()];

            this.badges = [new BadgeModel(`${season.episodes.length} Episodes`, null)];
        }

        if (episode) {
            this.cardType = 'episode';
            this.stillPath = episode.stillPath;
            this.backupStillPath = episode.season.series.backdropPath;
            this.imageWidth = 'w300';
            this.title = episode.title;
            this.linkPath = ['/items', 'episodes',  episode.id.toString()];
        }

        if (media) {
            this.cardType = 'media';
            if (media.movie) {
                this.stillPath = media.movie.posterPath;
                this.title = media.movie.title;
                this.linkPath = ['/items',  media.movie.id.toString()];
            } else {
                this.stillPath = media.episode.season.series.posterPath;
                this.title = media.episode.season.series.title;
                this.linkPath = ['/items',  media.episode.season.series.id.toString()];

                this.badges = [
                    new BadgeModel(
                        `Season ${media.episode.season.seasonNumber}`,
                        ['/items', 'seasons', media.episode.season.id.toString()]),
                    new BadgeModel(
                        `Episode ${media.episode.episodeNumber}`,
                        ['/items', 'episodes', media.episode.id.toString()])
                ];
            }
        }
    }
}

class BadgeModel {
    text: string;
    linkPath: string[];

    constructor(text: string, linkPath: string[]) {
        this.text = text;
        this.linkPath = linkPath;
    }
}
