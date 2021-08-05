import React, {useState} from "react";

export function UpdateBook({bookToUpdate, update}) {
  const [book, setBook] = useState({id: "", ...bookToUpdate});

  function onSubmit(e) {
    e.preventDefault();

    update(book);
  }

  function onChange(e) {
    const fieldName = e.target.name;
    const newValue = e.target.value;
    setBook((prevBook) => ({...prevBook, [fieldName]: newValue}));
  }

  return (
    <form onSubmit={onSubmit}>
      <h3>Update</h3>
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

      <button type="submit">Update</button>
    </form>
  );
}
