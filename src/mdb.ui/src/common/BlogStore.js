import BlogData from '../blogMaster.json'
import { getApiText } from '../common/ApiHelper';

/***** Posts *****/

export const getPosts = () => {
    var data = [];

    //fetch posts from all the blogs
    BlogData.posts.map(p => data.push(ConvertToPosts(p)));
    return data;
}

export const getPostById = (postId) => {
    var data = [];
    BlogData.posts.map(p => data.push(ConvertToPosts(p)));

    //filter by id
    let result = data.find(function (x) { return x.id === postId; });

    if(result === undefined)
        return null;

    return result;
}

export const getPostByTitleDescription = (searchTerm) => {
    var data = [];
    BlogData.posts.map(p => data.push(ConvertToPosts(p)));
    return data.filter(post => post.title.includes(searchTerm) || post.description.includes(searchTerm));
}

export const getPostByBlogSeries = (searchTerm) => {
    var data = [];
    BlogData.posts.map(p => data.push(ConvertToPosts(p)));
    return data.filter(post => post.series.title.toLowerCase() === searchTerm);
}

export const getPostBody = async (contentLocation, contentType, body) => {
    if (contentType === "embTxt") {
        return Promise.resolve(body);
    }
    return await getApiText(contentLocation);
}

/***** Authors *****/

export const getAuthorById = (authorId) => {
    //fetch author details
    return BlogData.authors.filter(x => x.id === authorId)[0];
}


function ConvertToPosts(post) {
    post.authors = []
    let authorInfo = post.authorIds;

    authorInfo.forEach(a => post.authors.push(getAuthorById(a)));

    return post;
}