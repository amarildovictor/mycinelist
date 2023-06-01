import './App.css';
import {cloneElement} from "react";
import Menu from './components/Menu';
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'

function App(props) {
  return (
    <>
    <Menu />
    {cloneElement(props.children)}
    
    <div className='d-flex justify-content-center mt-3 border-top'></div>
    <div className='d-flex justify-content-center mt-3 border-top border-4 bg-light'>© MyCineList - 2023 - Todos os direitos reservados.</div>
    </>
  );
}

export default App;
library.add(fas, far);