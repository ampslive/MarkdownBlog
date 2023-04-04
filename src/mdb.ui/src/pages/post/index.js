import React, { Fragment } from 'react'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
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
        let filteredPost = data.filter(function (x) { return x.id == id; });
        setPost(filteredPost[0]);
    }, [])

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
                <div class="container">
                    <div class="row">
                        <div class="col-md-11 my-4 borderRed">
                            <div>{blogPost.title}</div>
                            <div>{blogPost.author.name}</div>
                            <div>{blogPost.dateCreated}</div>
                            <div>{blogPost.bannerUri}</div>
                            <div>{blogPost.body}</div>
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