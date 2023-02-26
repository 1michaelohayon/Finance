import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useState } from "react";
import liabilitiesService from "./services/liabilities";
import usersService from "./services/users";
import LoginButton from "./componenets/login";

function App() {
  const [users, setUsers] = useState([]);
  const [liabilities, setLiabilities] = useState([]);
  const { getAccessTokenSilently } = useAuth0();

  const handleClick = async () => {
    const token = await getAccessTokenSilently();
    console.log(token);
    const users = await usersService.getAll(token);
    setUsers(users);
    console.log(users);
  };

  const handleLiabilitiesClick = async () => {
    const liabilities = await liabilitiesService.getAll();
    setLiabilities(liabilities);
    console.log(liabilities);
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <LoginButton />
        <button onClick={() => handleClick()}>click me</button>
        <div>
          {users.map((u: any) => (
            <div key={u.id}>{u.username}</div>
          ))}
        </div>
        <button onClick={() => handleLiabilitiesClick()}>click me2</button>
        <div>
          {liabilities.map((l: any) => (
            <div key={l.id}>{l.name}</div>
          ))}
        </div>
      </header>
    </div>
  );
}

export default App;
