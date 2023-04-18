import React, { Fragment } from 'react';
import { useState, useEffect } from 'react'
import ReactMarkdown from 'react-markdown';
import gfm from 'remark-gfm'
import { getPostBody } from '../../common/BlogStore';
import './style.css'

function PostBody(props) {

    const [postBody, setPostBody] = useState();

    const { contentLocation, contentType, body } = props.meta;

    useEffect(() => {
        getPostBody(contentLocation, contentType, body)
            .then(
                (text) => setPostBody(text)
            );
    }, [postBody, body, contentLocation, contentType]);

    return (
        <Fragment>
            <div class="text-justify lh-base">
                {
                    ((contentType === 'embTxt') && <p class='embTxt'>{postBody}</p> ) 
                    || <div class='mdContainer'><ReactMarkdown remarkPlugins={[gfm]} children={postBody} /></div>
                }
            </div>
        </Fragment>
    );
}

export default PostBody;