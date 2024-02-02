import React, { Fragment } from 'react'

function NoContent(props) {

    let message = props.message;

    return (
        <Fragment>
            <div class="container mainContent text-center m-3">
                <p class="fw-lighter fst-italic m-2">
                    {(message !== undefined) ? message : "No Data Found"}
                </p>
            </div>

        </Fragment>
    );
}

export default NoContent;