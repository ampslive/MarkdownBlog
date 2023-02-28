import React, { Fragment } from 'react'

function PostPreview(props) {
    return (
        <Fragment>
            <div class="card">
                <img src={props.image} class="card-img-top" alt="Image 1" />
                <div class="card-body">
                    <a class="nav-link card-title" href="/news"><h5>{props.title}</h5></a>
                    <p class="card-text">{props.description}</p>
                </div>
            </div>
        </Fragment>
    );
} 

export default PostPreview;