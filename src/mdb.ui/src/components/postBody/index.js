import React, { Fragment } from 'react';
import { useState, useEffect } from 'react'
import ReactMarkdown from 'react-markdown';
import gfm from 'remark-gfm'
import { getPostBody } from '../../common/BlogStore';
import './style.css'

function PostBody(props) {
    const [postBody, setPostBody] = useState();
    const body = props.post.Body;
    const { ContentLocation, ContentType } = props.post.Meta;

    useEffect(() => {
        getPostBody(ContentLocation, ContentType, body)
            .then(
                (text) => setPostBody(text)
            );
    }, [postBody, body, ContentLocation, ContentType]);

    return (
        <Fragment>
            <div class="text-justify lh-base">
                {
                    ((ContentType === 'embTxt') && <p class='embTxt'>{postBody}</p>)
                    || <div class='mdContainer'><ReactMarkdown remarkPlugins={[gfm]} children={postBody} /></div>
                }
            </div>
        </Fragment>
    );
}

export default PostBody;