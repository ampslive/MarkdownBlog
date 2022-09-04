import Container from 'react-bootstrap/Container'
import Navbar from 'react-bootstrap/Navbar';

function TopNav(props) {
    return (
        <Navbar bg="dark" variant="dark">
            <Container>
                <Navbar.Brand href="#home">{ props.title }</Navbar.Brand>
                <Navbar.Toggle />
                <Navbar.Collapse className="justify-content-end">
                    <Navbar.Text>
                        Signed in as: <a href="#login">Amit Philips</a>
                    </Navbar.Text>
                </Navbar.Collapse>
            </Container>
        </Navbar>

    );
}

export default TopNav;