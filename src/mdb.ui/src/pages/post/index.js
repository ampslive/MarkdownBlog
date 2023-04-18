import React, { Fragment } from 'react'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import './style.css'
import { getPostById } from '../../common/BlogStore'
import { formatDate } from '../../common/Utils';
import PostBody from '../../components/postBody';

function Post(props) {

    const defaultPost = {
        bannerUri: "https://picsum.photos/1000/300",
        blogName: "Blog Series",
        body: "This is my third blog post.",
        dateCreated: "09/17/2022 18:46:07",
        description: "Subtext",
        id: "0",
        title: "Sample Data",
        author: {
            id: 1,
            name: "Amit Philips",
            imageUri: "https://i.pravatar.cc/40?img=1"
        },
        meta: {
            body: "This is my third blog post.",
            contentType: "localMD",
            contentLocation: "../../md-posts/sample.md"
        }
    };

    const [blogPost, setPost] = useState(defaultPost);

    let { id } = useParams();


    useEffect(() => {
        let filteredPost = getPostById(id);
        setPost(filteredPost[0]);
    }, [id])

    return (
        <Fragment>
            {blogPost && (
                <div class="container mainContent">
                    <div class="row">
                        <div class="col-md-12 my-4 p-3 post">
                            <h2 class="text-center">{blogPost.title}</h2>
                            <div class="my-3">
                                <div class="d-flex justify-content-center"><img src={blogPost.author.imageUri} alt="author" class="author-image" /></div>
                                <div class="text-center"><p class="fw-semibold">{blogPost.author.name}</p></div>
                            </div>
                            <div class="text-center m-2"><p class="fw-light">{formatDate(blogPost.dateCreated) + '  |  ' + blogPost.blogName}</p></div>
                            <div><img src={blogPost.bannerUri} class="postBanner" alt='post-banner' /></div>
                            <div class="p-4">
                                <PostBody meta={blogPost.meta} />
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </Fragment>
    );
}

export default Post;