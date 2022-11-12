import React from 'react'
import BlogData from '../../blogsData.json'
import { useState, useEffect } from 'react'
import { Container, Row, Image } from 'react-bootstrap';

function Blog() {

    const [blogPosts, setPosts] = useState([]);

    useEffect(() => {
        var data = [];

        //fetch posts from all the blogs
        BlogData.blogs.map(x => x.posts.map(y => data.push(ConvertToPosts(x, y))));

        //BlogData.blogs.map(x => x.posts.map(y => console.log(ConvertToPosts(x,y))));

        //order posts by date descending
        data.sort((a, b) => Date.parse(b.dateCreated) - Date.parse(a.dateCreated));

        setPosts(data);
    }, [])

    function ConvertToDate(dt) {
        var date = new Date(dt);
        const options = { year: 'numeric', month: 'short', day: 'numeric' };

        return date.toLocaleDateString('en-US', options)
    }

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

        blogPosts &&
        blogPosts.map((post) =>

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
                    </div>
                    <Row xs={7}>
                        <article>{post.body}</article>
                    </Row>
                </Container>
            </div>


        )
    );
}

export default Blog;