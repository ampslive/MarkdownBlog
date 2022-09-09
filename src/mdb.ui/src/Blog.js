import React from 'react'
import BlogData from './blogsData.json'
import { useState, useEffect } from 'react'

function Blog() {

    const [blogs, setBlog] = useState([]);

    useEffect(() => {
        setBlog(BlogData.blogs);
        console.log(blogs)
    })

    return (
        <div>
            <h1>Blog Section</h1>
            {
                blogs.map((blog) =>
                    <div key={blog.Id}>
                        <h3>{blog.title}</h3>
                        <p>{blog.posts[0].title}</p>
                    </div>
                )
            }
        </div>
    );
}

export default Blog;