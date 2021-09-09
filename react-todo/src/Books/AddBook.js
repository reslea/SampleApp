import React, { useState } from "react";

export function AddBook({add}) {
  const [book, setBook] = useState({
    title: "",
    author: "",
    pagesCount: 0,
    publishDate: null,
  });

  function onSubmit(e) {
    e.preventDefault();

    add(book);
  }

  function onChange(e) {
    const fieldName = e.target.name;
    const newValue = e.target.value;
    setBook((prevBook) => ({...prevBook, [fieldName]: newValue}));
  }

  return (
    <form onSubmit={onSubmit}>
      <h3>Add</h3>
      <input name="title" value={book.title} onChange={onChange}/>
      <input name="author" value={book.author} onChange={onChange}/>
      <input
        name="pagesCount"
        type="number"
        value={book.pagesCount}
        onChange={onChange}
      />
      <input
        name="publishDate"
        type="date"
        value={book.publishDate}
        onChange={onChange}
      />

      <button type="submit">Add</button>
    </form>
  );
}
