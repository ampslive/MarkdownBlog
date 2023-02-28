import React from 'react'
import BlogData from '../../blogsData.json'
import { useState, useEffect } from 'react'
import { Container, Row, Image } from 'react-bootstrap';
import ReactMarkdown from 'https://esm.sh/react-markdown@7'
import remarkGfm from 'remark-gfm'
import PostPreview from '../../components/postPreview';

function Blog() {

    const [blogPosts, setPosts] = useState([]);
    const [mdpost, setMdPost] = useState([]);

    const markdown = "### LinkedList";



    //remarkPlugins={[remarkGfm]}

    useEffect(() => {
        var data = [];

        fetch('https://raw.githubusercontent.com/ampslive/DSA/main/DS/1-LinkedList/LinkedList.md')
            .then(response => response.text())
            //setMdPost(response);
            .then(data => setMdPost(data));

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
        <div class="container">
            <div class="row">
                {
                    blogPosts &&
                    blogPosts.map((post) =>
                        <div class="col-sm-4 my-4">
                            <PostPreview image={post.bannerUri} title={post.title} description={post.description} />
                        </div>
                    )
                }
            </div>
        </div>
    );
}

export default Blog;