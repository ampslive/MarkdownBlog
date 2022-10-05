import React from 'react'
import BlogData from '../../blogsData.json'
import { useState, useEffect } from 'react'
import { Container, Row, Image } from 'react-bootstrap';

function Blog() {

    const [blogPosts, setPosts] = useState([]);

    useEffect(() => {
        var data = [];

        //fetch posts from all the blogs
        BlogData.blogs.map(x => x.posts.map(y => data.push(y)));

        //order posts by date descending
        data.sort((a, b) => Date.parse(b.dateCreated) - Date.parse(a.dateCreated));

        setPosts(data);
    }, [])

    function ConvertToDate(dt) {
        var date = new Date(dt);
        const options = { year: 'numeric', month: 'short', day: 'numeric' };

        return date.toLocaleDateString('en-US', options)
    }

    return (

        blogPosts &&
        blogPosts.map((post) =>

            <div >
                <Row>
                    <Image src={post.bannerUri} fluid />
                </Row>
                <Container>
                    <Row>
                        <h6>BLOG-NAME</h6>
                    </Row>
                    <Row>
                        <h2>{post.title}</h2>
                    </Row>
                    <Row>
                        <p>{post.author.name} |  {ConvertToDate(post.dateCreated)}</p>
                    </Row>
                    <Row xs={7}>
                        <article>{post.body}</article>
                    </Row>
                </Container>
            </div>


        )
    );
}

export default Blog;