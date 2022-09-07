import React from "react";
import ListGroup from 'react-bootstrap/ListGroup';

class NewsSources extends React.Component {

    constructor() {
        super();
        this.state = { jokes: [] };

    }

    componentDidMount() {
        fetch(`http://api.icndb.com/jokes`)
            .then(result => result.json())
            .then(jokes => this.setState({ jokes : jokes.value.filter(function (joke) {
                return !joke.categories.includes('explicit');
            })}));
    }

    render() {
        return (
            <ListGroup as="ol" numbered>
                {this.state.jokes.map(joke =>
                    <ListGroup.Item as="li" key={joke.id}> {joke.joke} </ListGroup.Item> )} 
            </ListGroup>
        );
    }
}

export default NewsSources;