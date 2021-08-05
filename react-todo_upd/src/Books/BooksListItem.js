import React from "react";

export function BooksListItem({book, remove, setUpdateId}) {
  return (
    <>
      <li key={book.id}>
        {book.title}
        <span role="img" aria-label="delete" onClick={
          () => remove(book.id)
        }>
          ❌
        </span>
        <span role="img" aria-label="update" onClick={() => {
          setUpdateId(book.id);
        }}
        >
          ✏️
        </span>
      </li>
    </>
  );
}
