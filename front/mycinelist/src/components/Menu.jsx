import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Form from 'react-bootstrap/Form';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import $ from 'jquery';
import { useNavigate, useParams } from 'react-router-dom';
import { logout } from '../api/utils';

export default function Menu(props) {
  const navigate = useNavigate();
  const { movieTimelineRelease } = useParams();

  switch (movieTimelineRelease) {
    case "COMING_SOON":
      $("#comingSoonLinkMenu").addClass("active");
      break;
    case "PREMIERES":
      $("#premieresLinkMenu").addClass("active");
      break;
    case "ON_DISPLAY":
      $("#onDisplayLinkMenu").addClass("active");
      break;
    case "IN_FAREWELL":
      $("#inFarewellLinkMenu").addClass("active");
      break;
    default:
      $("#basic-navbar-nav .active").removeClass("active");
      break;
  }

  const submitSearch = (e) => {
    e.preventDefault();

    var searchTextInput = $('#searchText').val();

    window.location.href = `/search?searchText=${searchTextInput}`;
  }

  function doLogout() {
    logout();
    props.setLogged(false);
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
            <Nav.Link id='homeLinkMenu' href="/">Home</Nav.Link>
            <Nav.Link id='comingSoonLinkMenu' href="/search/timeline/COMING_SOON">Em Breve</Nav.Link>
            <Nav.Link id='premieresLinkMenu' href="/search/timeline/PREMIERES">Estreias</Nav.Link>
            <Nav.Link id='onDisplayLinkMenu' href="/search/timeline/ON_DISPLAY">Em Cartaz</Nav.Link>
            <Nav.Link id='inFarewellLinkMenu' href="/search/timeline/IN_FAREWELL">Em despedida</Nav.Link>
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
          {props.logged ?
            <NavDropdown title={<FontAwesomeIcon icon='fa-solid fa-user' className='ms-3 mt-2 fs-5 text-primary' title='Usuário logado' />}
              id="basic-nav-dropdown">
              <NavDropdown.Item onClick={() => navigate("/userThings/userList")} className='text-black'>Minha lista</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item onClick={doLogout} className='text-black'>Logout</NavDropdown.Item>
            </NavDropdown>
            :
            <NavDropdown title={<FontAwesomeIcon icon='fa-solid fa-user' className='ms-3 mt-2 fs-5 text-danger' title='Usuário não logado' />}
              id="basic-nav-dropdown">
              <NavDropdown.Item onClick={() => navigate("/auth/login")} className='text-black'>Login</NavDropdown.Item>
              <NavDropdown.Item onClick={() => navigate("/auth/register")} className='text-black'>Cadastrar-se</NavDropdown.Item>
            </NavDropdown>
          }
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}