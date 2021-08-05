const booksApiUrl = "https://localhost:5001/api/books";

export function getAll(rawToken) {
  return fetch(booksApiUrl, {
    headers: {
      Authorization: `Bearer ${rawToken}`,
    },
  })
    .then((response) => response.json())
}

export function remove(id, rawToken) {
  return fetch(`${booksApiUrl}/${id}`, {
    method: "delete",
    headers: {
      Authorization: `Bearer ${rawToken}`,
    },
  })
    .then(response => {
      if (response.status !== 204) {
        throw new Error("remove failed");
      }
      return null
    })
}

export function add(book, rawToken) {
  return fetch(booksApiUrl, {
    method: "post",
    headers: {
      Authorization: `Bearer ${rawToken}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(book),
  })
    .then((response) => {
      if (response.status === 200) {
        return response.json();
      } else throw new Error("addBook failed");
    })
}

export function update(book, rawToken) {
  return fetch(`${booksApiUrl}/${book.id}`, {
    method: "put",
    headers: {
      Authorization: `Bearer ${rawToken}`,
      "Content-Type": "application/json",
    },
    body: JSON.stringify(book),
  })
    .then((response) => {
      if(response.status === 204)
        return null
      else throw new Error("update failed");
    })
}
