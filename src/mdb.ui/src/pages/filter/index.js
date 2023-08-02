import React, { Fragment } from 'react'
import { getPostByBlogSeries } from '../../common/BlogStore'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import PostPreview from '../../components/postPreview';
import JumbotronFilter from '../../components/jumbotronFilter';
import {capitalize} from '../../common/Utils'

function Filter() {
    let { filter, searchTerm } = useParams();

    const [blogPosts, setPosts] = useState([]);

    useEffect(() => {

        async function LoadData() {
            var data = [];

            if(filter === 'series') {
                data = await getPostByBlogSeries(searchTerm.toLowerCase());
            }
            else if (filter === 'author') {
                data = await getPostByBlogSeries(searchTerm.toLowerCase());
            }

            //order posts by date descending
            data.sort((a, b) => Date.parse(b.DateCreated) - Date.parse(a.DateCreated));

            setPosts(data);
        }

        LoadData();

    }, [searchTerm])


    return (
        <Fragment>
            <div class="container">
                <div class="row p-5 bg-dark">
                 {blogPosts && <JumbotronFilter Filter={capitalize(filter)} Title={blogPosts[0]?.Series?.Title} Description={blogPosts[0]?.Series?.Description} Stats={blogPosts.length} /> }
                </div>
                <div class="row">
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

export default Filter;