import React from 'react'
import { Container, Row, Image } from 'react-bootstrap';
import { useState, useEffect } from 'react'

function Post(props) {

    const [post, setPost] = useState([]);

    useEffect(() => {
        var data = props.post;
        setPost(data);
    }, [])

    return (

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
    );
}

export default Post;