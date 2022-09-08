import Container from 'react-bootstrap/Container'
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import { Link } from 'react-router-dom'

function TopNav(props) {
    return (
        <Navbar bg="dark" variant="dark" expand="sm" fixed="top">
            <Container>
                <Navbar.Brand href="/">{props.title}</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link as={Link} to="/news">News</Nav.Link>
                        <Nav.Link as={Link} to="/blog">Blog</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default TopNav;