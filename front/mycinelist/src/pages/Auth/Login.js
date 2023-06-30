import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Form from 'react-bootstrap/Form';
import FloatingLabel from 'react-bootstrap/esm/FloatingLabel';
import Button from 'react-bootstrap/Button';
import { Link, useNavigate } from 'react-router-dom';
import $ from 'jquery';
import { authSessions, validateEmail } from '../../api/utils';
import { getAxiosApiServer } from './../../api/axiosBase';

export default function Login(props) {
    const navigate = useNavigate();

    if (props.logged) {
        window.location.href = "/";
    }

    const matchPassValidate = () => {
        var passwordTxtVal = $('#passwordTxt').val();
        var isValid = true;

        if ($('#userEmailTxt').val().trim() === '' || !validateEmail($('#userEmailTxt').val())) {
            $('#userEmailTxt').addClass('is-invalid');
            $('#userEmailTxt').removeClass('is-valid');
            isValid = false;
        }
        else {
            $('#userEmailTxt').removeClass('is-invalid');
            $('#userEmailTxt').addClass('is-valid');
        }

        if (passwordTxtVal.trim() === '') {
            $('#passwordTxt').addClass('is-invalid');
            $('#passwordTxt').removeClass('is-valid');
            isValid = false;
        }
        else {
            $('#passwordTxt').removeClass('is-invalid');
            $('#passwordTxt').addClass('is-valid');
        }

        return isValid;
    }

    const loginApi = async (user) => {
        await getAxiosApiServer().post('/v1/Auth/login', user)
            .then(function (response) {
                user = response.data;
                $('#messagesDiv').hide();
                props.setLogged(true);
                navigate('/');
                authSessions(user);
            })
            .catch(function (error) {
                fillAlertMessages(error);
            })
    };

    function fillAlertMessages(error) {
        let data = error.response.data;
        let messagesDiv = $('#messagesDiv');
        let messages = messagesDiv.find('ul');

        messagesDiv.show();
        messages.empty();
        for (let key in data)
            messages.append(`<li>${data[key]}</li>`);
    }

    function entrar_onClick() {
        if (matchPassValidate()) {
            loginApi({ email: $('#userEmailTxt').val().trim(), password: $('#passwordTxt').val() });
        }
    }

    return (
        <Form className='my-3' onSubmit={props.handleSubmit}>
            <div className="row text-center">
                <h5 className='fw-bold'>
                    <FontAwesomeIcon icon="fa-solid fa-user" className='me-2 text-primary' />
                    Login
                </h5>
            </div>
            <div className="row d-flex justify-content-center">
                <div className='border border-success rounded shadow bg-light p-3' style={{ maxWidth: '350px' }}>
                    <div id="messagesDiv" className='text-danger' style={{ display: 'none' }}>
                        <ul></ul>
                    </div>
                    <FloatingLabel controlId="userEmailTxt" label="E-mail" className='mb-2'>
                        <Form.Control type="email" placeholder="E-mail" required />
                        <Form.Control.Feedback type="invalid" id='emailInvalidMessage'>
                            Insira um e-mail válido
                        </Form.Control.Feedback>
                    </FloatingLabel>
                    <FloatingLabel controlId="passwordTxt" label="Senha" className='mb-2'>
                        <Form.Control type="password" placeholder="Senha" required />
                        <Form.Control.Feedback type="invalid" id='passwordInvalidMessage'>
                            Insira a senha
                        </Form.Control.Feedback>
                    </FloatingLabel>
                    <div className="d-flex justify-content-center">
                        <Button variant="success" type="submit" onClick={entrar_onClick}>
                            <FontAwesomeIcon icon="fa-solid fa-right-to-bracket" className='me-2' />
                            Entrar
                        </Button>
                    </div>
                    <div className="d-flex justify-content-center pt-1">
                        <Link to='/auth/register' className='text-decoration-none'>
                            Ainda não possui uma conta? Cadastre-se aqui.
                        </Link>
                    </div>
                </div>
            </div>
        </Form>
    )
}