import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Form from 'react-bootstrap/Form';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import $ from 'jquery';
import { useNavigate, useParams } from 'react-router-dom';
import { logout } from '../api/utils';
import { getUserSession } from '../api/utils';

export default function Menu(props) {
  const navigate = useNavigate();
  const { movieTimelineRelease } = useParams();

  const getActiveLinkMenu = (linkMenu) => {
    if ((window.location.pathname ==='/' && linkMenu ==='HOME') || movieTimelineRelease === linkMenu) {
      return "link-success";
    }
    else
      return null;
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
    <Navbar bg='light' expand="xl">
      <Container>
        <Navbar.Brand href="/">
          <FontAwesomeIcon className='me-2' icon="fa-solid fa-film" />
          <span className='fw-bold'>
            <span className='text-primary'>My</span>
            <span className='text-success'>Cine</span>
            <span className='text-danger'>List</span>
          </span>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="offcanvasNavbar-expand-lg" />
        <Navbar.Offcanvas id="offcanvasNavbar-expand-lg" aria-labelledby="offcanvasNavbarLabel-expand-lg" placement="end">
          <Offcanvas.Body>
            <Nav className="me-auto">
              <Nav.Link id='homeLinkMenu' href="/" className={getActiveLinkMenu('HOME')}>Home</Nav.Link>
              <Nav.Link id='comingSoonLinkMenu' href="/search/timeline/COMING_SOON" className={getActiveLinkMenu('COMING_SOON')}>Em Breve</Nav.Link>
              <Nav.Link id='premieresLinkMenu' href="/search/timeline/PREMIERES" className={getActiveLinkMenu('PREMIERES')}>Estreias</Nav.Link>
              <Nav.Link id='onDisplayLinkMenu' href="/search/timeline/ON_DISPLAY" className={getActiveLinkMenu('ON_DISPLAY')}>Em Cartaz</Nav.Link>
              <Nav.Link id='inFarewellLinkMenu' href="/search/timeline/IN_FAREWELL" className={getActiveLinkMenu('IN_FAREWELL')}>Em despedida</Nav.Link>
            </Nav>
            <Form className="d-flex">
              <Form.Control
                id='searchText'
                type="search"
                placeholder="Buscar"
                className="me-2"
                aria-label="Search"
              />
              <Button variant="success" type="submit" onClick={submitSearch}>
                <FontAwesomeIcon icon="fa-solid fa-magnifying-glass" />
              </Button>
            </Form>
            <hr />
            <Nav>
              {props.logged ?
                <NavDropdown
                  title={
                    <>
                      <FontAwesomeIcon icon='fa-solid fa-user' className='me-2 text-primary' title='Usuário logado' />
                      {getUserSession().userEmail}
                    </>
                  }>
                  <NavDropdown.Item onClick={() => navigate("/userThings/userList")} className='text-black'>Minha lista</NavDropdown.Item>
                  <NavDropdown.Divider />
                  <NavDropdown.Item onClick={doLogout} className='text-black'>Logout</NavDropdown.Item>
                </NavDropdown>
                :
                <NavDropdown
                  title={
                    <>
                      <FontAwesomeIcon icon='fa-solid fa-user' className='me-2 text-danger' title='Usuário não logado' />
                      Entrar
                    </>
                  }>
                  <NavDropdown.Item onClick={() => navigate("/auth/login")} className='text-black'>Login</NavDropdown.Item>
                  <NavDropdown.Item onClick={() => navigate("/auth/register")} className='text-black'>Cadastrar-se</NavDropdown.Item>
                </NavDropdown>
              }
            </Nav>
          </Offcanvas.Body>
        </Navbar.Offcanvas>
      </Container>
    </Navbar>
  )
}