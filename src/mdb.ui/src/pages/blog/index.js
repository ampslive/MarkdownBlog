import React from 'react'
import BlogData from '../../blogsData.json'
import { useState, useEffect } from 'react'
import Image from 'react-bootstrap/Image'
import Card from 'react-bootstrap/Card';

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

    return (

        blogPosts &&
        blogPosts.map((post) =>
            <Card key={post.id}>
                <Card.Img variant="top" src={post.bannerUri} />
                <Card.Body >
                    <Card.Title>{post.title}</Card.Title>
                    <Card.Text>{post.body}</Card.Text>
                </Card.Body>
            </Card>
        )
    );
}

export default Blog;