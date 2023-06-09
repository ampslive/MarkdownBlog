import BlogData from '../blogMaster.json'
import { getApiText, getApiJson } from '../common/ApiHelper';

/***** Posts *****/

const LoadMasterData = async () => {
    var data = [];

    var response = await getApiJson("https://mdbstore.blob.core.windows.net/bm1/blogMaster.json");

    //fetch posts from all the blogs
    response.Posts.map(p => data.push(ConvertToPosts(p)));
    return data;
}

export const getPosts = async () => {
    //fetch posts from all the blogs
    var data = await LoadMasterData();

    return data;
}

export const getPostById = async (postId) => {
    var data = await LoadMasterData();

    //filter by id
    let result = data.find(function (x) { return x.Id === postId; });

    if (result === undefined)
        return null;

    return result;
}

export const getPostByTitleDescription = async (searchTerm) => {
    var data = await LoadMasterData();
    return data.filter(post => post.Title.includes(searchTerm) || post.Description.includes(searchTerm));
}

export const getPostByBlogSeries = async (searchTerm) => {
    var data = await LoadMasterData();
    return data.filter(post => post.Series.Title.toLowerCase() === searchTerm);
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
    post.Authors = []
    let authorInfo = post.AuthorIds;

    authorInfo.forEach(a => post.Authors.push(getAuthorById(a)));
    return post;
}