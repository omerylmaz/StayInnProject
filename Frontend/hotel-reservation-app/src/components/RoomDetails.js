import React, { useState, useEffect } from 'react';
import axios from 'axios';

const RoomDetails = ({ roomId }) => {
  const [room, setRoom] = useState(null);

  useEffect(() => {
    axios.get(`/rooms/${roomId}`)
      .then(response => {
        setRoom(response.data);
      })
      .catch(error => {
        console.error("Error fetching room details:", error);
      });
  }, [roomId]);

  return (
    <div>
      {room ? (
        <div>
          <h1>{room.name}</h1>
          <p>Price: {room.price}</p>
          <p>Description: {room.description}</p>
        </div>
      ) : <p>Loading...</p>}
    </div>
  );
}

export default RoomDetails;
