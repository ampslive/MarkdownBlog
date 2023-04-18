import React, { Fragment } from 'react'
import { useState, useEffect } from 'react'
//import ReactMarkdown from 'https://esm.sh/react-markdown@7'
//import remarkGfm from 'remark-gfm'
import PostPreview from '../../components/postPreview';
import './style.css'
import Jumbotron from '../../components/jumbotron'
import { getPosts } from '../../common/BlogStore'

function Blog() {

    const [blogPosts, setPosts] = useState([]);
    //const [heroPost, setHero] = useState([]);
    //const [mdpost, setMdPost] = useState([]);

    //remarkPlugins={[remarkGfm]}

    useEffect(() => {
        var data = [];

        data = getPosts();

        //order posts by date descending
        data.sort((a, b) => Date.parse(b.dateCreated) - Date.parse(a.dateCreated));

        setPosts(data);
    }, [])


    return (
        <Fragment>
            <div class="p-5 bg-dark">
                <Jumbotron />
            </div>
            <div class="container">
                <div class="row">
                    {
                        blogPosts &&
                        blogPosts.map((post) =>
                            <div class="col-sm-4 my-4" key={post.id}>
                                <PostPreview post={post} />
                            </div>
                        )
                    }
                </div>
            </div>
        </Fragment>
    );
}

export default Blog;