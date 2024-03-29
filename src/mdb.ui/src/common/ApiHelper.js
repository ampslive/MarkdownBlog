export const getApiJson = (url) => {

    return fetch(url)
        .then(result => result.json());

}

export const postApiJson = (url) => {

    return fetch(url)
        .then(result => result.json());

}

export const getApiText = (url) => {
    if(url === undefined)
        return;

    return fetch(url)
        .then(result => result.text());
}