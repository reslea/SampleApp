import { useState } from "react";

export function useFetch(url) {
  const [data, setData] = useState();
  const [error, setError] = useState();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setIsLoading(true);
    fetch(url, { 
      method: 'POST', 
      body: JSON.stringify(loginData),
      headers: {
        'Content-Type': 'application/json'
      },
    })
    .then(r => r.json())
    .then(data => setData(data))
    .catch(err => setError(err))
    .finally(() => setIsLoading(false))
  }, []);

  return {
    data,
    isLoading,
    error
  }
}