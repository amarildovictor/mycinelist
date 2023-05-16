import './App.css';
import Menu from './components/Menu';
import Home from './pages/Home';
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'

function App(props) {
  return (
    <>
    <div>
      <Menu />

      <Home />
    </div>
    <div className='d-flex justify-content-center mt-3 border-top'>© MyCineList - 2023 - Todos os direitos reservados.</div>
    </>
  );
}

export default App;
library.add(fas, far);