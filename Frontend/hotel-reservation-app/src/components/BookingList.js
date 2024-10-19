import React, { useState, useEffect } from 'react';
import axios from 'axios';

const BookingList = ({ bookingName }) => {
  const [bookings, setBookings] = useState([]);

  useEffect(() => {
    axios.get(`/bookings/${bookingName}`)
      .then(response => {
        setBookings(response.data);
      })
      .catch(error => {
        console.error("Error fetching bookings:", error);
      });
  }, [bookingName]);

  return (
    <ul>
      {bookings.map(booking => (
        <li key={booking.id}>{booking.details}</li>
      ))}
    </ul>
  );
}

export default BookingList;
