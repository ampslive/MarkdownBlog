import React from 'react'
import './style.css'

function Jumbotron() {
    return (
        <div class="jumbotron jumbotron-fluid text-white">
            <div class="container p-4 text-center">
                <h1 class="display-4">Hello, world! I'm Amit</h1>
                <p class="lead">I'm a passionate coder who thrives on turning ideas into reality using the power of .NET and Azure.</p>
                <hr class="my-4" />
                <p>On this blog, you'll find my musings on coding, detailed walkthroughs, and insights on building applications. Whether you're a fellow coder or someone interested in learning more about the world of programming, I hope you'll find something of interest here.</p>
                <p class="lead">
                    {/* <button class="btn btn-primary btn-lg">About Me</button> */}
                </p>
            </div>
        </div>
    );

}

export default Jumbotron;