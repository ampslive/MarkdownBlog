export const formatDate = (date) => {
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    let dt = new Date(Date.parse(date));
    let currentDate = new Date();

    let timeDiff = Math.abs(currentDate.getTime() - dt.getTime());
    let diffDays = Math.floor(timeDiff / (1000 * 3600 * 24));
    //alert(diffDays);
    switch (true) {
        case (diffDays === 0):
            return 'Today';

        case (diffDays === 1):
            return 'Yesterday';

        case (diffDays < 7):
            return diffDays + 'd ago';

        case (diffDays >= 7 && diffDays <= 13):
            return '1wk ago';

        default:
            return new Intl.DateTimeFormat('en-US', options).format(dt);

    }
}

export const capitalize = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
}