import React from 'react'
import './style.css'

function Sample(props) {

    return (
        <div>
            {/* <div class="row">
                <img src="https://picsum.photos/1000/300" fluid />
            </div> */}

            <div class="jumbotron jumbotron-fluid jumbotron-custom p-3">
                <div class="container">
                    <h1 class="display-4">Hello, world!</h1>
                    <p class="lead">This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
                    <hr class="my-4" />
                    <p>It uses utility classes for typography and spacing to space content out within the larger container.</p>
                    <p class="lead">
                        <a class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
                    </p>
                </div>
            </div>

            <div class="container">
                <div class="row">
                    <div class="col-sm-4 my-4">
                        <div class="card">
                            <img src="https://picsum.photos/300/100" class="card-img-top" alt="Image 1" />
                            <div class="card-body">
                                <a class="nav-link card-title" href="/news"><h5>Title 1</h5></a>
                                <p class="card-text">Some text about image 1</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4  my-4">
                        <div class="card">
                            <img src="https://picsum.photos/300/100" class="card-img-top" alt="Image 1" />
                            <div class="card-body">
                                <a class="nav-link card-title" href="#"><h5>Title 1</h5></a>
                                <p class="card-text">Some text about image 1</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4  my-4">
                        <div class="card">
                            <img src="https://picsum.photos/300/100" class="card-img-top" alt="Image 1" />
                            <div class="card-body">
                                <a class="nav-link card-title" href="#"><h5>Title 1</h5></a>
                                <p class="card-text">Some text about image 1</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4  my-4">
                        <div class="card">
                            <img src="https://picsum.photos/300/100" class="card-img-top" alt="Image 1" />
                            <div class="card-body">
                                <a class="nav-link card-title" href="#"><h5>Title 1</h5></a>
                                <p class="card-text">Some text about image 1</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    );
}
export default Sample;