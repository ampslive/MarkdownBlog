import React from 'react'

function SocialsShare(props) {

    const shareOnFacebook = () => {
        alert(document.location.href);
        const postUrl = encodeURI(document.location.href);
        const postTitle = encodeURI("Your Blog Post Title Here");
        const postDesc = encodeURI("Your Blog Post Description Here");
        const postImg = encodeURI("URL of the image to display in the share");
       // window.open(`https://www.facebook.com/sharer.php?s=100&p[u]=${postUrl}`, 'sharer', 'toolbar=0,status=0,width=548,height=325');

        window.open(`https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Flocalhost%3A3000%2Fpost%2Fg7PfYC6ycE2hC1TUTd5bCQ%2Ffourth-post-title%23&amp;src=sdkpreparse`, 'sharer', 'toolbar=0,status=0,width=548,height=325');
    }

    const shareOnTwitter = () => {
        const postUrl = encodeURI(document.location.href);
        const postTitle = encodeURI("Your Blog Post Title Here");
        window.open(`https://twitter.com/share?url=${postUrl}&text=${postTitle}`, 'sharer', 'toolbar=0,status=0,width=548,height=325');
    }

    const shareOnLinkedIn = () => {
        const postUrl = encodeURI(document.location.href);
        window.open(`https://www.linkedin.com/shareArticle?mini=true&url=${postUrl}`, 'sharer', 'toolbar=0,status=0,width=548,height=325');
    }

    return (
        <>
            <h5>Spread the Knowledge</h5>
            
            <div class="col-md-5 mx-auto btn-group">
            <iframe src="https://www.facebook.com/plugins/share_button.php?href=https%3A%2F%2Fwww.amitphilips.com%2Fpost%2Fhu2DkDY_RkSlRCDdB1lbJg%2Ffifth-post-title&layout&size&appId=278634362300633&width=77&height=20" width="77" height="20" styleName="border:none;overflow:hidden" scrolling="no" frameborder="0" allowfullscreen="true" allow="autoplay; clipboard-write; encrypted-media; picture-in-picture; web-share"></iframe>
                <a href="#" class="btn btn-outline-primary rounded" onClick={shareOnFacebook}>Share on Facebook</a>
                <a href="#" onClick={shareOnTwitter}>Share on Twitter</a>
                <a href="#" onClick={shareOnLinkedIn}>Share on LinkedIn</a>
                <button type="button" class="btn btn-outline-primary rounded">Share <i class="bi bi-facebook"></i></button>
                <button type="button" class="btn btn-outline-primary rounded mx-1">Tweet <i class="bi bi-twitter-x"></i></button>
                <button type="button" class="btn btn-outline-primary rounded mx-1">Post <i class="bi bi-threads"></i></button>
                <button type="button" class="btn btn-outline-primary rounded mx-1">Post <i class="bi bi-mastodon"></i></button>
                <button type="button" class="btn btn-outline-primary rounded">Post <i class="bi bi-linkedin"></i></button>
            </div>
        </>
    );
}

export default SocialsShare;