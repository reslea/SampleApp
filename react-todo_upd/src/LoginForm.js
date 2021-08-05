import React, { useState, useContext } from 'react';
import { TokenContext } from './index';
import { Redirect } from "react-router-dom";
import { getTokenData, tokenStorage } from './utilities/token';

export default function LoginForm() {
  const [email, setEmail] = useState('testuser@gmail.com');
  const [password, setPassword] = useState('password');
  
  let [tokenData, setToken] = useContext(TokenContext);

  const authApiLink = "https://localhost:6001/api/auth";

  function login(e) {
    e.preventDefault();

    const loginData = { email, password };
    
    fetch(authApiLink, { 
      method: 'POST', 
      body: JSON.stringify(loginData),
      headers: {
        'Content-Type': 'application/json'
      },
    })
      .then(response => response.json())
      .then(tokenResponse => {
        const token = tokenResponse.jwtToken;
        tokenStorage.set(token);
        getTokenData(setToken, token);
      })
      .catch(() => console.error('auth failed'));
  }

  return tokenData
    ? (<Redirect to="/"></Redirect>)
    : (
    <div>
      <form noValidate>
        <input  type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />

        <button onClick={login}>Login</button>
      </form>
    </div>)
}