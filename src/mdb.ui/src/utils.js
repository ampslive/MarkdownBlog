export const formatDate = (date) => {
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    var dt = new Date( Date.parse(date));
    return new Intl.DateTimeFormat('en-US', options).format(dt);
}