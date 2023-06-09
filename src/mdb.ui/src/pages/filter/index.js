import React, { Fragment } from 'react'
import { getPostByBlogSeries } from '../../common/BlogStore'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import PostPreview from '../../components/postPreview';

function Filter() {
    let { searchSeries } = useParams();

    const [blogPosts, setPosts] = useState([]);

    useEffect(() => {

        async function LoadData() {

            var data = await getPostByBlogSeries(searchSeries.toLowerCase());

            //order posts by date descending
            data.sort((a, b) => Date.parse(b.DateCreated) - Date.parse(a.DateCreated));

            setPosts(data);
        }

        LoadData();

    }, [searchSeries])


    return (
        <Fragment>
            <div class="container">
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