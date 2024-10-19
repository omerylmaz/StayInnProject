import React, { useState } from 'react';
import axios from 'axios';

const CreateBooking = () => {
  const [bookingData, setBookingData] = useState({});

  const handleInputChange = (e) => {
    setBookingData({
      ...bookingData,
      [e.target.name]: e.target.value
    });
  }

  const handleSubmit = () => {
    axios.post('/bookings', bookingData)
      .then(response => {
        console.log("Booking created:", response.data);
      })
      .catch(error => {
        console.error("Error creating booking:", error);
      });
  }

  return (
    <div>
      <input type="text" name="roomId" placeholder="Room ID" onChange={handleInputChange} />
      <input type="text" name="userId" placeholder="User ID" onChange={handleInputChange} />
      <button onClick={handleSubmit}>Create Booking</button>
    </div>
  );
}

export default CreateBooking;
