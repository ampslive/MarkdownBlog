import React, { Fragment } from 'react'
import './style.css'

function PostPreview(props) {
    return (
        <Fragment>
            <div class="card">
                <img src={props.post.bannerUri} class="card-img-top" alt="Post Banner" />
                <div class="card-body">
                    <pre class="text-right text-uppercase">{props.post.blogName}</pre>
                    <a class="nav-link card-title" href="/news"><h5>{props.post.title}</h5></a>
                    <p class="card-text">{props.post.description}</p>
                    
                    <div class="d-flex justify-content-between text-muted">
                        <div class="d-flex justify-content-between">
                            <img src={props.post.author.imageUri} alt="author" class="user-image"/>
                            <p class="px-2 mt-1">{props.post.author.name}</p>
                        </div>
                        <div><p class="px-2 mt-1"><small>{props.post.dateCreated}</small></p></div>
                    </div>

                </div>
            </div>
        </Fragment>
    );
} 