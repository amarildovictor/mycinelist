import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Form from 'react-bootstrap/Form';
import Navbar from 'react-bootstrap/Navbar';
import Button from 'react-bootstrap/Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import $ from 'jquery';

export default function Menu() {

  const submitSearch = (e) => {
    e.preventDefault();

    var searchTextInput = $('#searchText').val();

    window.location.href = `/search?searchText=${searchTextInput}`;
  }

  return (
  <Navbar bg='light' expand="lg">
    <Container>
      <Navbar.Brand href="/">
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
          <Nav.Link href="/">Home</Nav.Link>
          <Nav.Link href="/search/timeline/COMING_SOON">Em Breve</Nav.Link>
          <Nav.Link href="/search/timeline/PREMIERES">Estreias</Nav.Link>
          <Nav.Link href="/search/timeline/ON_DISPLAY">Em Cartaz</Nav.Link>
          <Nav.Link href="/search/timeline/IN_FAREWELL">Em despedida</Nav.Link>
        </Nav>
        <Form className="d-flex">
          <Form.Control
            id='searchText'
            type="search"
            placeholder=""
            className="me-2"
            aria-label="Search"
          />
          <Button variant="success" type="submit" onClick={submitSearch}>
            <FontAwesomeIcon icon="fa-solid fa-magnifying-glass" />
          </Button>
        </Form>
      </Navbar.Collapse>
    </Container>
  </Navbar>
  )
}