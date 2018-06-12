export class AppSettings {

    public static posterPlaceHolder = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABsAAAAoCA'
        + 'IAAACAWBYBAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcE'
        + 'hZcwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA'
        + '0LjAuMjHxIGmVAAAAMUlEQVRIS+3MMQ0AIBAAMcSQX96/P0x045LOPXfW'
        + 'ajQajUaj0Wg0Go1Go9FoNH4cZx++Tu2oMm5kFAAAAABJRU5ErkJggg==';

    public static stillPlaceHolder = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGQAAAA4CAI'
        + 'AAACEzMqPAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZ'
        + 'cwAADsMAAA7DAcdvqGQAAAAZdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0L'
        + 'jAuMjHxIGmVAAAAiElEQVR4Xu3QQQ0AIAwAMcQQPvPvDwu7f5Mq6LlvWJ'
        + 'IVyApkBbICWYGsQFYgK5AVyApkBbICWYGsQFYgK5AVyApkBbICWYGsQFY'
        + 'gK5AVyApkBbICWYGsQFYgK5AVyApkBbICWYGsQFYgK5AVyApkBbICWYGs'
        + 'QFYgK5AVyApkBbICWYGsQNbamw9fYv/oWnfhAgAAAABJRU5ErkJggg==';

    public static imageUrl(imagePath: string, size: string = 'original'): string {
        return `https://image.tmdb.org/t/p/${size}${imagePath}`;
    }
}
