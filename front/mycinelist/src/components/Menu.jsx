import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Form from 'react-bootstrap/Form';
import Navbar from 'react-bootstrap/Navbar';
import Button from 'react-bootstrap/Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

export default function Menu() {
    return (
    <Navbar bg='light' expand="lg">
      <Container>
        <Navbar.Brand href="#home">
          <FontAwesomeIcon className='me-2' icon="fa-solid fa-film" />
          <span className='fw-bold'>
            <span className='text-primary'>My</span>
            <span className='text-success'>Cine</span>
            <span className='text-danger'>List</span>
          </span>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#link">Em Breve</Nav.Link>
            <Nav.Link href="#link">Estreias</Nav.Link>
            <Nav.Link href="#link">Em Cartaz</Nav.Link>
            <Nav.Link href="#link">Em despedida</Nav.Link>
          </Nav>
          <Form className="d-flex">
            <Form.Control
              type="search"
              placeholder=""
              className="me-2"
              aria-label="Search"
            />
            <Button variant="success">
              <FontAwesomeIcon icon="fa-solid fa-magnifying-glass" />
            </Button>
          </Form>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    )
}