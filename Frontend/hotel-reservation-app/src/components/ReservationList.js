import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ReservationList = ({ userId }) => {
  const [reservations, setReservations] = useState([]);

  useEffect(() => {
    axios.get(`/reservation/${userId}`)
      .then(response => {
        setReservations(response.data);
      })
      .catch(error => {
        console.error("Error fetching reservations:", error);
      });
  }, [userId]);

  return (
    <ul>
      {reservations.map(reservation => (
        <li key={reservation.id}>{reservation.details}</li>
      ))}
    </ul>
  );
}

export default ReservationList;
