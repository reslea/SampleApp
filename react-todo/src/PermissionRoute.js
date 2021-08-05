import React, { useContext } from 'react';
import { TokenContext } from '.';
import { Route, Redirect } from "react-router-dom";
import { tokenStorage, getTokenData } from './utilities/token';
import PermissionRequired from './PermissionRequired';

export default function PermissionRoute({ permission, ...rest }) {
  let [tokenData, setTokenData] = useContext(TokenContext);

  if(!tokenData) {
    const token = tokenStorage.get();
    if(token) {
      tokenData = getTokenData(setTokenData, token);

      const isExpired = tokenData.expires > new Date()
      if(isExpired) {
        console.log('redirect to login');
        return <Redirect to="/login"></Redirect>
      }
    }
    else {
      console.log('redirect to login');
      return <Redirect to="/login"></Redirect>
    }
  }

  if (!tokenData?.permissions.includes(permission)) {
    console.log('redirect to permission required');
    return <PermissionRequired />
  }

  console.log('permission routing passed');
  return <Route {...rest} />
}