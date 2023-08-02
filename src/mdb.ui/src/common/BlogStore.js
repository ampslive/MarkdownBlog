import { getApiText, getApiJson } from '../common/ApiHelper';

const cacheNameBlogMaster = 'blogMaster';
const blogMasterUri = 'https://mdbstore.blob.core.windows.net/bm1/blogMaster.json';

/***** Posts *****/

const LoadMasterData = async () => {
    var data = [];
    const cacheDuration = 60 * 60 * 1000; // 1 hour in milliseconds

    const cachedData = localStorage.getItem(cacheNameBlogMaster);

    if (cachedData) {
        const { data, timestamp } = JSON.parse(cachedData);
        if (Date.now() - timestamp < cacheDuration) {
          return data;
        }
    }

    //if cache has expired or not available
    var response = await getApiJson(blogMasterUri);

    let authorList = response.Authors;
    
    //fetch posts from all the blogs
    response.Posts.map(p => data.push(ConvertToPosts(p, authorList)));

    localStorage.setItem(cacheNameBlogMaster, JSON.stringify({ data, timestamp: Date.now() }))
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

export const getAuthorById = (authorId, authorList) => {
    //fetch author details
    var authorDetails = authorList.filter(x => x.Id === authorId)[0];
    return authorDetails;
}

/***** BlogSeries *****/

export const getBlogSeriesById = (seriesTitle, seriesList) => {
    
}

function ConvertToPosts(post, authorList) {
    post.Authors = []
    let authorInfo = post.AuthorIds;
    authorInfo.forEach(a => post.Authors.push(getAuthorById(a, authorList)));
    return post;
}