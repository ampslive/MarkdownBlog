import React, { Fragment } from 'react'
import { useState, useEffect } from 'react'
import PostPreview from '../../components/postPreview';
import './style.css'
import Jumbotron from '../../components/jumbotron'
import { getPosts } from '../../common/BlogStore'
import NoContent from '../../components/NoContent'

function Blog(props) {

    const [blogPosts, setPosts] = useState([]);

    useEffect(() => {

        var data = [];

        async function LoadData() {
            data = await getPosts();

            data.sort((a, b) => Date.parse(b.DateCreated) - Date.parse(a.DateCreated));

            setPosts(data);
        }
        LoadData();

        //order posts by date descending

    }, [])


    return (
        <Fragment>
            <div class="bg-dark">
                <Jumbotron />
            </div>
            <div class="container">
                <div class="row">
                    { (!blogPosts || blogPosts.length === 0) && <NoContent message="No posts found" />}

                    {
                        blogPosts &&
                        blogPosts.map((post) =>
                            <div class="col-sm-4 my-4" key={post.Id}>
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