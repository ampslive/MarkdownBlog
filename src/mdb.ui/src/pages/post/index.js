import React, { Fragment } from 'react'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import './style.css'
import BlogData from '../../blogsData.json'

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
        }
    };

    const [blogPost, setPost] = useState(defaultPost);
    let { id } = useParams();


    useEffect(() => {

        var data = [];
        BlogData.blogs.map(x => x.posts.map(y => data.push(ConvertToPosts(x, y))));
        let filteredPost = data.filter(function (x) { return x.id === id; });
        setPost(filteredPost[0]);
    }, [id])

    function ConvertToPosts(blog, post) {
        return {
            id: post.id,
            blogName: blog.title,
            title: post.title,
            bannerUri: post.bannerUri,
            description: post.description,
            body: post.body,
            dateCreated: post.dateCreated,
            author: {
                id: post.author.id,
                name: post.author.name,
                imageUri: post.author.imageUri
            }
        }
    }




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
                            <div class="text-center m-2"><p class="fw-light">{blogPost.dateCreated + '  |  ' + blogPost.blogName}</p></div>
                            <div><img src={blogPost.bannerUri} class="postBanner" alt='post-banner' /></div>
                            <div class="p-4"><p class="text-justify lh-base">{blogPost.body}</p></div>
                        </div>
                    </div>
                </div>
            )}
        </Fragment>

        /*
        post &&
        (
            <div >
                <Row>
                    <Image src={post.bannerUri} fluid />
                </Row>
                <Container>
                    <div>
                        <Row>
                            <h6>{post.blogName.toUpperCase()}</h6>
                        </Row>
                        <Row>
                            <h2>{post.title}</h2>
                        </Row>
                        <Row>
                            <p>{post.author.name} |  {ConvertToDate(post.dateCreated)}</p>
                        </Row>
                        <Row>
                            <ReactMarkdown children={mdpost} remarkPlugins={[remarkGfm]} /> 
                        </Row>
                    </div>
                    <Row xs={7}>
                        <article>{post.body}</article>
                    </Row>
                </Container>
            </div> 
        )
        */
    );
}

export default Post;