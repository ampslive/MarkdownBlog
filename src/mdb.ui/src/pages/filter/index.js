import React, {Fragment} from 'react'
import { getPostByBlogSeries } from '../../common/BlogStore'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';
import PostPreview from '../../components/postPreview';

function  Filter() {
    let { searchSeries } = useParams();

    const [blogPosts, setPosts] = useState([]);

    useEffect(() => {

        var data = [];

        data = getPostByBlogSeries(searchSeries.toLowerCase());

        //order posts by date descending
        data.sort((a, b) => Date.parse(b.dateCreated) - Date.parse(a.dateCreated));

        setPosts(data);
    }, [searchSeries])


    return (
        <Fragment>
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

export default Filter;