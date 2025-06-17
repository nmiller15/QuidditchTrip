import c from 'picocolors';

export function log(message) {
    const now = new Date();
    const timeString = now.toTimeString().split(' ')[0];
    console.log(`${c.dim(timeString)} ${c.cyan("[INFO]")} - ${message}`);
}
