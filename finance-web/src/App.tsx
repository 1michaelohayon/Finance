import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useState } from "react";
import liabilitiesService from "./services/liabilities";
import usersService from "./services/users";
import LoginButton from "./componenets/login";
import useField from "./hooks/useField";
import CreateLiability from "./componenets/ CreateLiability";

function App() {
  const [users, setUsers] = useState([]);
  const [liabilities, setLiabilities] = useState([]);
  const [liab, setLiability] = useState("");

  const searchById = useField("text");
  const { getAccessTokenSilently, getIdTokenClaims } = useAuth0();

  const handleClick = async () => {
    const token = await getAccessTokenSilently();
    const claims = await getIdTokenClaims();
    console.log("claims", claims);
    const users = await usersService.getAll(token);
    setUsers(users);
    console.log(users);
  };

  const handleLiabilitiesClick = async () => {
    const liabilities = await liabilitiesService.getAll();
    setLiabilities(liabilities);
    console.log(liabilities);
  };

  const searchByIdClick = async () => {
    const liability = await liabilitiesService.getByUserId(
      searchById.input.value
    );
    setLiability(liability.name);
    console.log(liability);
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

        <input {...searchById.input} />
        <button onClick={() => searchByIdClick()}>search by id</button>
        {liab}
      </header>
      <CreateLiability />
    </div>
  );
}

export default App;
