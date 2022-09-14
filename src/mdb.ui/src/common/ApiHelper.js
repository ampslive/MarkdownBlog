export const GetApi = function ApiHelper(url) {

    return fetch(url)
        .then(result => result.json());

}

export const PostApi = function ApiHelper(url) {

    return fetch(url)
        .then(result => result.json());

}

