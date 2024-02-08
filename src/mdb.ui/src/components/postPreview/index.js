import React, { Fragment } from 'react'
import './style.css'
import { Link } from 'react-router-dom';
import { formatDate } from '../../common/Utils';
import UserImage from '../../components/userImage';

function PostPreview(props) {
    const postTitle = props.post.Title.trim().toLowerCase().replace(/\s/g, "-");
    const postUri = `/post/${props.post.Id}/` + postTitle;
    const seriesUri = `/blog/series/${props.post.Series.Title.toLowerCase()}/`;

    return (
        <Fragment>
            <div class="card">
                <Link to={postUri}>
                    <img src={props.post.BannerUri} class="card-img-top" alt="Post Banner" />
                </Link>

                <div class="card-body">
                    <Link to={seriesUri} class="nav-link card-title">
                        <pre class="text-right text-uppercase">{props.post.Series.Title}</pre>
                    </Link>

                    <Link to={postUri} class="nav-link card-title">
                        <h5>{props.post.Title}</h5>
                    </Link>

                    <div class="my-2">
                        <p class="card-text">{props.post.Description}

                        <Link to={postUri} class="nav-link card-title">
                            <span>[...]</span>
                        </Link></p>
                    </div>

                    <div class="row px-2 text-muted">
                        <div class="col-1 p-0">
                            <UserImage image={props.post.Authors[0]?.ImageUri} />
                        </div>
                        <div class="col-7"><p class="mt-1">{props.post.Authors[0]?.Name}</p></div>
                        <div class="col-4"><p class=" mt-1 float-end"><small>{formatDate(props.post.DatePublished)}</small></p></div>
                    </div>

                </div>
            </div>
        </Fragment>
    );
}

export default PostPreview;
