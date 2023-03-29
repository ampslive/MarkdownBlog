import React, { Fragment } from 'react'
import './style.css'
import { Link } from 'react-router-dom';

function PostPreview(props) {

    const postUri = `/post/${props.post.id}`;

    return (
        <Fragment>
            <div class="card">
                <Link to={postUri}>
                    <img src={props.post.bannerUri} class="card-img-top" alt="Post Banner" />
                </Link>

                <div class="card-body">
                    <pre class="text-right text-uppercase">{props.post.blogName}</pre>

                    <Link to={postUri} class="nav-link card-title">
                        <h5>{props.post.title}</h5>
                    </Link>

                    <p class="card-text">{props.post.description}</p>

                    <div class="d-flex justify-content-between text-muted">
                        <div class="d-flex justify-content-between">
                            <img src={props.post.author.imageUri} alt="author" class="user-image" />
                            <p class="px-2 mt-1">{props.post.author.name}</p>
                        </div>
                        <div><p class="px-2 mt-1"><small>{props.post.dateCreated}</small></p></div>
                    </div>

                </div>
            </div>
        </Fragment>
    );
}

export default PostPreview;
