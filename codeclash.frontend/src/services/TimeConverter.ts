export function convertTime(time: string): string {
    let seconds: string;
    let minutes = '';

    if (time.includes('m')){
        const [minutesPart, secondsPart] = time.split(' ');
        minutes = minutesPart.replace('m', '');
        seconds = secondsPart.replace('s', '');
    }
    else
        seconds = time.replace('s', '')

    return `00:${minutes}:${seconds}`;
}