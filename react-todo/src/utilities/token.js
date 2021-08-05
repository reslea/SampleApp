export function getTokenData(setToken, token) {
  const payloadBase64 = token.split('.')[1];
  const payload = JSON.parse(atob(payloadBase64));

  const permissions = Array.isArray(payload.permission) ? payload.permission : [payload.permission];

  const result = {
    email: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
    permissions, 
    expires: new Date(payload.exp * 1000),
    token: token
  }

  setToken(result);
  return result;
}

const tokenKey = 'token';
function saveToStorage(token) {
  localStorage.setItem(tokenKey, token);
}

function getFromStorage() {
  return localStorage.getItem(tokenKey);
}

export const tokenStorage = (function() {
  let isTokenChecked = false;
  let token = null;

  return {
    set(tokenValue) {
      token = tokenValue;
      saveToStorage(tokenValue);
    },
    get() {
      if(!isTokenChecked) {
        token = getFromStorage();
        isTokenChecked = true;
      }
      return token;
    }
  }
})();

export const permissions = { 
  readBooks: 'ReadBooks',
  editBooks: 'EditBooks'
}