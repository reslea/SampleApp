import React, { useContext } from 'react';
import { TokenContext } from '.';

export default function RequirePermission ({requiredPermission, ...rest }) {
  
  const [tokenData] = useContext(TokenContext);

  if (!tokenData?.permissions.includes(requiredPermission)) {
    return <span>NOT AUTHORIZED, you dont have {requiredPermission} permission</span>
  }
  return { ...rest.children }
}