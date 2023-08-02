import React from 'react'

function JumbotronFilter(props) {


    return (
        <div class="jumbotron jumbotron-fluid text-white">
            <div class="container text-center">
                <h1 class="display-4">{props.Filter}: {props.Title}</h1>
                <p class="lead">{props.Description}</p>
                <hr class="my-4" />
                <p class="fw-light fst-italic">Posts: {props.Stats}</p>
            </div>
        </div>
    );

}

export default JumbotronFilter;