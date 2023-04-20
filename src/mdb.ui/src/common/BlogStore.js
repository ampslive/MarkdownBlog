import BlogData from '../blogMaster.json'
import { getApiText } from '../common/ApiHelper';

/***** Posts *****/

export const getPosts = () => {
    var data = [];

    //fetch posts from all the blogs
    BlogData.blogs.map(x => x.posts.map(y => data.push(ConvertToPosts(x, y))));
    return data;
}

export const getPostById = (postId) => {
    var data = [];
    BlogData.blogs.map(x => x.posts.map(y => data.push(ConvertToPosts(x, y))));

    //filter by id
    return data.filter(function (x) { return x.id === postId; });
}

export const getPostByTitleDescription = (searchTerm) => {
    var data = [];
    BlogData.blogs.map(x => x.posts.map(y => data.push(ConvertToPosts(x, y))));
    return data.filter(post => post.title.includes(searchTerm) || post.description.includes(searchTerm));
}

export const getPostByBlogSeries = (searchTerm) => {
    var data = [];
    BlogData.blogs.map(x => x.posts.map(y => data.push(ConvertToPosts(x, y))));
    return data.filter(post => post.blogName.toLowerCase() === searchTerm);
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


function ConvertToPosts(blog, post) {

    let authors = [];
    post.authors.map(a=> authors.push(getAuthorById(a.id)));

    let postData = {
        id: post.id,
        blogName: blog.title,
        title: post.title,
        bannerUri: post.bannerUri,
        description: post.description,
        meta: {
            body: post.body,
            contentType: post.meta.contentType,
            contentLocation: post.meta.contentLocation
        },
        dateCreated: post.dateCreated,
        author: authors
    }

    return postData;
}