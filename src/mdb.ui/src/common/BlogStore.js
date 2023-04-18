import BlogData from '../blogMaster.json'
import { getApiText } from '../common/ApiHelper';

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

export const getPostBody = async (contentLocation, contentType, body) => {
    if (contentType === "embTxt") {
        return Promise.resolve(body);
    }
    return await getApiText(contentLocation);
}


function ConvertToPosts(blog, post) {
    return {
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
        author: {
            id: post.author.id,
            name: post.author.name,
            imageUri: post.author.imageUri
        }
    }
}