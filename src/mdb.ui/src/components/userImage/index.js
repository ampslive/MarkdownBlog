import React from 'react'

function UserImage(props) {

    const imageUri = props.image;

    return (
        <>
            {(imageUri === null || imageUri === undefined) ?
                <img src="/assets/person-circle.svg" alt="author" style={{width: "100%", height: "auto"}}/> :
                (<img src={imageUri} alt="author" class="rounded-circle" style={{width: "100%", height: "auto"}} />)
            }
        </>
    );
}

export default UserImage;