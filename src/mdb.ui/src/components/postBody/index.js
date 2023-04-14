import React, { Fragment } from 'react';
import { useState, useEffect } from 'react'
import ReactMarkdown from 'react-markdown';
import { getApiText } from '../../common/ApiHelper';

function PostBody(props) {

    const [postMd, setPostMd] = useState();

    const { contentLocation, contentType, body } = props.meta;

    useEffect(() => {
        if (contentType === "localMD" || contentType === "extMD") {
            getApiText(contentLocation)
                .then((text) => {
                    setPostMd(text);
                });
        }
    });

    return (
        <Fragment>
            <div class="text-justify lh-base">
            {
                (contentType === 'embTxt') ? <p>{body}</p> : <ReactMarkdown children={postMd} />
            }
            </div>
        </Fragment>
    );
}

export default PostBody;