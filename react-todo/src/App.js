import React, { useState } from 'react';
import './App.css';
import { TokenContext } from '.';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Main from './Main';
import LoginForm from './LoginForm';
import PermissionRoute from './PermissionRoute';
import { permissions } from './utilities/token';



function App() {
  const [token, setToken] = useState();

  return (
    <div className="App">    
      <TokenContext.Provider value={[token, setToken]}>
        <Router>
          <header>some header</header>
          <Switch>
            <PermissionRoute exact path="/" component={Main} permission={permissions.readBooks} />
            <Route path="/login" component={LoginForm} />
          </Switch>
        </Router>
      </TokenContext.Provider>
    </div>
  );
}



export default App;