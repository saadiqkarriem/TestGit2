import React, { useState } from "react";
import "./Home.css";
import axios from 'axios';

function Home() {
  const [FirstName, setFirstName] = useState("");
  const [LastName, setLastName] = useState("");
  const [Age, setAge] = useState("");
  const [personalinfo, setpersonalinfo] = useState([]);
  const handleSubmit = async (event) => {
    event.preventDefault();
    const newUser = { FirstName, LastName, Age };
    try {
      const response = await axios.post('https://localhost:7284/api/Personalinfo', newUser);
      setpersonalinfo([...personalinfo, response.data]);
      setFirstName("");
      setLastName("");
      setAge("");
    } catch (error) {
      console.log(error);
    }
  };

  const handleView = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.get('https://localhost:7284/api/Personalinfo');
      setpersonalinfo(response.data);
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <div className="container">
      <h2 className="heading">Personal Information</h2>
      <form onSubmit={handleSubmit} className="form">
        <label>
          Name:
          <input
            type="text"
            value={FirstName}
            onChange={(event) => setFirstName(event.target.value)}
          />
        </label><br></br>
        <label><br></br>
          Surname:
          <input
            type="text"
            value={LastName}
            onChange={(event) => setLastName(event.target.value)}
          />
        </label><br></br>
        <label><br></br>
          Age:
          <input
            type="number"
            value={Age}
            onChange={(event) => setAge(event.target.value)}
          />
        </label><br></br>
        <div>
          <button type="submit">Add</button>
          <button type="button" onClick={handleView}>
            View
          </button>
        </div>
      </form>
      <div className="user-list">
        <h2>Stored Users:</h2>
        {personalinfo.length > 0 ? (
          <ul>
            {personalinfo.map((user, index) => (
              <li key={index}>
                <p>Name: {user.firstName}</p>
                <p>Surname: {user.lastName}</p>
                <p>Age: {user.age}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p>No users found.</p>
        )}
      </div>
    </div>
  );
}
export default Home; 