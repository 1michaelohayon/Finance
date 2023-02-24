import { useState } from 'react';
import usersService from './services/users';
import { useAuth0 } from '@auth0/auth0-react';
import liabilitiesService from './services/liabilities';
import LoginButton from './components/LoginButton';

const App = () => {
  const [users, setUsers] = useState([]);
  const [liabilities, setLiabilities] = useState([]);
  const { getAccessTokenSilently } = useAuth0();

  const handleClick = async () => {
    const token = await getAccessTokenSilently();
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
        <LoginButton />
        <button onClick={() => handleClick()}>click me</button>
        <div>
          {users.map(u => <div key={u.id}>{u.username}</div>)}
        </div>
        <button onClick={() => handleLiabilitiesClick()}>click me2</button>
        <div>
          {liabilities.map(l => <div key={l.id}>{l.name}</div>)}
        </div>
      </header>
    </div>
  );
}

export default App;
