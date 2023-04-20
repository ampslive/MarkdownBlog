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
        author: [{
            id: 1,
            name: "Amit Philips",
            imageUri: "https://i.pravatar.cc/40?img=1"
        }],
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
                                <div class="d-flex justify-content-center"><img src={blogPost.author[0].imageUri} alt="author" class="author-image" /></div>
                                <div class="text-center"><a href="#postFooter" class="fw-semibold link-secondary">{blogPost.author[0].name}</a></div>
                            </div>
                            <div class="text-center m-2"><p class="fw-light">{formatDate(blogPost.dateCreated) + '  |  ' + blogPost.blogName}</p></div>
                            <div><img src={blogPost.bannerUri} class="postBanner" alt='post-banner' /></div>
                            <div class="p-4">
                                <PostBody meta={blogPost.meta} />
                            </div>
                        </div>
                    </div>
                    <div id="postFooter" class="row p-4">
                        <div class="col-md-10 mx-auto">
                            <div class="d-flex flex-row">
                                <div class="m-2"><img src={blogPost.author[0].imageUri} alt="author" class="author-image-footer" /></div>
                                <div class="m-2">
                                    <h6>ABOUT {blogPost.author[0].name.toUpperCase()}</h6>
                                    <p>{blogPost.author[0].bio}</p>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            )}
        </Fragment>
    );
}

export default Post;