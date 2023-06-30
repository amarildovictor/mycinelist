import './App.css';
import { cloneElement, useEffect, useState } from "react";
import Menu from './components/Menu';
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'

function App(props) {
  const [logged, setLogged] = useState(false);

  useEffect(() => {
    if (localStorage.getItem('authToken')) {
      setLogged(true);
    }
  }, [setLogged]);

  const handleSubmit = (event) => {
    event.preventDefault();
    event.stopPropagation();
  };

  return (
    <>
      <Menu logged={logged} setLogged={setLogged} />
      {cloneElement(props.children, { logged: logged, setLogged: setLogged, handleSubmit: handleSubmit })}

      {/* <div className='d-flex justify-content-center mt-3 border-top'></div> */}
      <div className='d-flex justify-content-center mt-3 border-top border-4 bg-light'>© MyCineList - 2023 - Todos os direitos reservados.</div>
    </>
  );
}

export default App;
library.add(fas, far);