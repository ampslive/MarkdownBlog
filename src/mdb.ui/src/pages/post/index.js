import React from 'react'
//import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom';

function Post(props) {

    /*
    const [post, setPost] = useState([]);

    useEffect(() => {
        var data = props.post;
        setPost(data);
    }, [])
    */

    let { id } = useParams();

    return (

        <div>Post Id: {id}</div>
        /*
        post &&
        (
            <div >
                <Row>
                    <Image src={post.bannerUri} fluid />
                </Row>
                <Container>
                    <div>
                        <Row>
                            <h6>{post.blogName.toUpperCase()}</h6>
                        </Row>
                        <Row>
                            <h2>{post.title}</h2>
                        </Row>
                        <Row>
                            <p>{post.author.name} |  {ConvertToDate(post.dateCreated)}</p>
                        </Row>
                        <Row>
                            <ReactMarkdown children={mdpost} remarkPlugins={[remarkGfm]} /> 
                        </Row>
                    </div>
                    <Row xs={7}>
                        <article>{post.body}</article>
                    </Row>
                </Container>
            </div> 
        )
        */
    );
}

export default Post;