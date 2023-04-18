import React, { Fragment } from 'react';
import { useState, useEffect } from 'react'
import ReactMarkdown from 'react-markdown';
import { getPostBody } from '../../common/BlogStore';

function PostBody(props) {

    const [postBody, setPostBody] = useState();

    const { contentLocation, contentType, body } = props.meta;

    useEffect(() => {
        getPostBody(contentLocation, contentType, body)
            .then(
                (text) => setPostBody(text)
            );
    }, [postBody]);

    return (
        <Fragment>
            <div class="text-justify lh-base">
                {
                    ((contentType === 'embTxt') && <p>{postBody}</p> ) 
                    || <ReactMarkdown children={postBody} />
                }
            </div>
        </Fragment>
    );
}

export default PostBody;