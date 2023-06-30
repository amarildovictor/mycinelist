import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import FloatingLabel from 'react-bootstrap/esm/FloatingLabel';
import $ from 'jquery';
import { getAxiosApiServer } from '../../api/axiosBase';
import { authSessions, validateEmail } from '../../api/utils';
import { Link, useNavigate } from 'react-router-dom';

export default function Register(props) {
    const navigate = useNavigate();

    if (props.logged) {
        window.location.href = "/";
    }

    const matchPassValidate = () => {
        var passwordTxtVal = $('#passwordTxt').val();
        var confirmPasswordTxt = $('#confirmPasswordTxt');
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

        if (passwordTxtVal !== confirmPasswordTxt.val()) {
            confirmPasswordTxt.addClass('is-invalid');
            confirmPasswordTxt.removeClass('is-valid');
            isValid = false;
        }
        else {
            confirmPasswordTxt.removeClass('is-invalid');
            confirmPasswordTxt.addClass('is-valid');
        }

        return isValid;
    }

    const registerUser = async (user) => {
        await getAxiosApiServer().post('/v1/Account', user)
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

    function cadastrar_onClick() {
        if (matchPassValidate()) {
            registerUser({ email: $('#userEmailTxt').val().trim(), password: $('#passwordTxt').val() });
        }
    }

    return (
        <Form className='my-3' onSubmit={props.handleSubmit}>
            <div className="row text-center">
                <h5 className='fw-bold'>
                    <FontAwesomeIcon icon="fa-solid fa-user-plus" className='me-2 text-primary' />
                    Cadastro de usuário
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
                    <FloatingLabel controlId="confirmPasswordTxt" label="Confirmação de senha" className='mb-2'>
                        <Form.Control type="password" placeholder="Confirmação de senha" required />
                        <Form.Control.Feedback type="invalid" id='confirmPasswordInvalidMessage'>
                            Os campos de Senhas não correspondem-se
                        </Form.Control.Feedback>
                    </FloatingLabel>
                    <div className="d-flex justify-content-center">
                        <Button variant="success" type="submit" onClick={cadastrar_onClick}>
                            <FontAwesomeIcon icon="fa-solid fa-right-to-bracket" className='me-2' />
                            Cadastrar-me
                        </Button>
                    </div>
                    <div className="d-flex justify-content-center pt-1">
                        <Link to='/auth/login' className='text-decoration-none'>
                            Já possui uma conta? Entre aqui.
                        </Link>
                    </div>
                </div>
            </div>
        </Form>
    )
}