import React from 'react'

function Jumbotron() {
    return (
        <div class="jumbotron jumbotron-fluid text-white">
            <div class="container text-center">
                <h1 class="display-4">Hello, world!</h1>
                <p class="lead">This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
                <hr class="my-4" />
                <p>It uses utility classes for typography and spacing to space content out within the larger container.</p>
                <p class="lead">
                    <button class="btn btn-primary btn-lg">Learn more</button>
                </p>
            </div>
        </div>
    );

}

export default Jumbotron;