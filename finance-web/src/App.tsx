import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { useAuth0 } from "@auth0/auth0-react";
import { useState } from "react";
import liabilitiesService from "./services/liabilities";
import LoginButton from "./componenets/login";
import useField from "./hooks/useField";
import CreateLiability from "./componenets/ CreateLiability";

function App() {
  const [liabilities, setLiabilities] = useState([]);

  const { getAccessTokenSilently } = useAuth0();

  const handleLiabilitiesClick = async () => {
    const token = await getAccessTokenSilently();
    const liabilities = await liabilitiesService.getAll(token);
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

        <button onClick={() => handleLiabilitiesClick()}>
          Get liabilities
        </button>
        <div>
          {liabilities.map((l: any) => (
            <div key={l.id}>{l.name}</div>
          ))}
        </div>
      </header>
      <CreateLiability />
    </div>
  );
}

export default App;
