import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './pages/Home';
import RoomList from './components/RoomList'

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/rooms" element={<RoomList />} />
      </Routes>
    </Router>
  );
}

export default App;
