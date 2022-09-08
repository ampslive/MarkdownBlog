import React from 'react'
import BlogData from './blogsData.json'

class Blog extends React.Component {
    constructor() {
        super();
        this.state = { blogs: [] };
    }

    componentDidMount() {
        console.log(BlogData);
        this.setState({ blogs: BlogData.blogs });
    }

    render() {
        return (
            <div>
                <h1>Blog Section</h1>
                {
                    this.state.blogs.map((blog) =>
                        <div key={blog.Id}>
                            <h3>{blog.title}</h3>
                            <p>{blog.posts[0].title}</p>
                        </div>

                    )}
            </div>
        );
    }
}

export default Blog;