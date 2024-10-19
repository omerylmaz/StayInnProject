import React, { useState } from 'react';
import axios from 'axios';

const CreateReservation = () => {
  const [reservationData, setReservationData] = useState({});

  const handleInputChange = (e) => {
    setReservationData({
      ...reservationData,
      [e.target.name]: e.target.value
    });
  }

  const handleSubmit = () => {
    axios.post('/reservation', reservationData)
      .then(response => {
        console.log("Reservation created:", response.data);
      })
      .catch(error => {
        console.error("Error creating reservation:", error);
      });
  }

  return (
    <div>
      <input type="text" name="userId" placeholder="User ID" onChange={handleInputChange} />
      <input type="text" name="roomId" placeholder="Room ID" onChange={handleInputChange} />
      <button onClick={handleSubmit}>Create Reservation</button>
    </div>
  );
}

export default CreateReservation;
