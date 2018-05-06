export class AppSettings {
    public static imageUrl(imagePath: string, size: string = "original"): string {
        return `https://image.tmdb.org/t/p/${size}${imagePath}`;
    }
}