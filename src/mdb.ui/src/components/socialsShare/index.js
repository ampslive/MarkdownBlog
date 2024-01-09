import React from 'react'
import { FacebookShareButton, FacebookIcon, TwitterShareButton, XIcon, WhatsappShareButton, WhatsappIcon, LinkedinShareButton, LinkedinIcon } from "react-share";

function SocialsShare(props) {

    const post = props.post;
    const postUrl = document.location.href.replace("http://localhost:3000/", "https://amitphilips.com/");
    console.log(document);

    return (
        <>
            <h5>Spread the Knowledge</h5>

            <div class="col-md-3 mx-auto">
                <div class="row justify-content-between">
                    <div class="col">
                        <FacebookShareButton
                            url={postUrl}
                            quote={post.Description}>
                            <FacebookIcon size={36} round />
                        </FacebookShareButton>
                    </div>
                    <div class="col">
                        <TwitterShareButton
                            url={postUrl}
                            title={post.Title}>
                            <XIcon size={32} round />
                        </TwitterShareButton>
                    </div>
                    <div class="col">
                        <WhatsappShareButton
                            url={postUrl}
                            title={post.Title}
                            separator=":: ">
                            <WhatsappIcon size={32} round />
                        </WhatsappShareButton>
                    </div>
                    <div class="col">
                        <LinkedinShareButton
                            url={postUrl}>
                            <LinkedinIcon size={32} round />
                        </LinkedinShareButton>
                    </div>
                </div>
            </div>
        </>
    );
}

export default SocialsShare;