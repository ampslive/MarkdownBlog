import React, { Fragment } from 'react'
import { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom';
import './style.css'
import { getPostById } from '../../common/BlogStore'
import { formatDate } from '../../common/Utils';
import PostBody from '../../components/postBody';
import NoContent from '../../components/NoContent';
import Socials from '../../components/socials';
import UserImage from '../../components/userImage';
import SocialsShare from '../../components/socialsShare';
import { Helmet } from 'react-helmet-async';

function Post(props) {

    const defaultPost = {
        DateCreated: "09/17/2022 18:46:07",
        Authors: [{ Name: "abc", ImageUri: "" }],
        Meta: {},
        Series: { Title: "" },
        seriesUri: ""
    };

    const [blogPost, setPost] = useState(defaultPost);
    const [isBusy, setBusy] = useState(true);

    let { id } = useParams();
    const seriesUri = `/blog/series/`;
    const authorUri = `/blog/author/`;

    useEffect(() => {

        async function LoadData() {
            let filteredPost = await getPostById(id);

            setPost(filteredPost);
        }

        LoadData();

    }, [id])

    return (
        <Fragment>
            {!blogPost && <NoContent />}

            {blogPost && (
                <div class="container mainContent">
                    <Helmet>
                        <title>{blogPost.Title}</title>
                        <meta name="description" content={blogPost.Description} />
                        <meta name="og:title" content={blogPost.Title} />

                        <meta property="og:title" content={`${blogPost.Title}`} />
                        <meta property="og:type" content="article" />
                        <meta property="og:description" content={`${blogPost.Description}`} />
                        <meta property="og:image" content={`https://www.amitphilips.com/assets/person-circle.svg`} />
                    </Helmet>
                    <div class="row">
                        <div class="col-md-12 my-4 p-3 post">
                            <h2 class="text-center">{blogPost.Title}</h2>
                            <div class="my-3">
                                <div class="d-flex justify-content-center"><img src={blogPost.Authors[0]?.ImageUri} alt="author" class="author-image" /></div>
                                <div class="text-center"><a href="#postFooter" class="fw-semibold link-secondary">{blogPost.Authors[0]?.Name}</a></div>
                            </div>
                            <div class="d-flex justify-content-center text-center m-2">
                                <p class="fw-light">{formatDate(blogPost.DateCreated) + '  |  '}</p>
                                <Link to={seriesUri + blogPost.Series.Title.toLowerCase() + '/'} class="mx-1 nav-link card-title">
                                    <p class="fw-light">{blogPost.Series.Title}</p>
                                </Link>
                            </div>
                            <div>
                                {isBusy && (<span class="loader"></span>)}
                                <img src={blogPost.BannerUri} class="postBanner" onLoad={() => setBusy(false)} alt='post-banner' />
                            </div>
                            <div class="p-4">
                                <PostBody post={blogPost} />
                            </div>
                        </div>
                    </div>
                    <div class="row text-center my-3">
                        <SocialsShare post={blogPost} />
                    </div>
                    <div id="postFooter" class="row p-4">
                        <div class="col-md-10 mx-auto">
                            <div class="d-flex flex-row">
                                <div class="m-2" style={{ flex: "0 0 15%" }}><UserImage class="author-image-footer" image={blogPost.Authors[0]?.ImageUri} /></div>
                                <div class="m-2" style={{ flex: "0 0 85%" }}>
                                    <h6>ABOUT {blogPost.Authors[0]?.Name.toUpperCase()}</h6>
                                    <p>{blogPost.Authors[0]?.Bio}</p>
                                    <Socials provider={blogPost.Authors[0]?.Socials} />
                                    <Link to={authorUri + blogPost.Authors[0]?.Id + '/'} class="nav-link">
                                        <p class="fw-light">Posts by this author</p>
                                    </Link>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            )}
        </Fragment>
    );
}

export default Post;