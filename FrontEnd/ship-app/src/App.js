import { BrowserRouter, Route, Routes } from 'react-router-dom';

import Home from './components/home';
import Listing from './components/listing';
import './App.css';

function App() {
  return (
    <main>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="listing" element={<Listing />} />
      </Routes>
    </main>
  )
}

export default App;
