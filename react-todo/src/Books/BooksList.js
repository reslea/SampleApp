import React, {useCallback, useContext, useEffect, useReducer, useState} from "react";
import {TokenContext} from "..";
import {UpdateBook} from "./UpdateBook";
import {AddBook} from "./AddBook";
import {add, getAll, remove, update} from "./requests";
import {
  addBookAction,
  booksReducer,
  getBooksAction,
  initialState,
  removeBookAction,
  updateBookAction
} from "./booksReducer";
import {BooksListItem} from "./BooksListItem";

export default function BooksList() {
  const [tokenData] = useContext(TokenContext);

  const [{ books }, dispatch] = useReducer(booksReducer, initialState);

  const [updateBookId, setUpdateBookId] = useState();

  function fetchData() {
    if (!tokenData) return;

    getAll(tokenData.token)
      .then(books => dispatch(getBooksAction(books)));
  }

  const removeBook = useCallback(async id => {
      try {
        await remove(id, tokenData.token);
        dispatch(removeBookAction(id));
      } catch (err) {
        alert(err)
      }
    }, [tokenData.token]
  );

  const addBook = useCallback(newBook => {
      add(newBook, tokenData.token)
        .then(added => dispatch(addBookAction(added)))
        .catch(err => alert(err));
    }, [tokenData.token]
  );

  const updateBook = useCallback(updatedBook => {
      update(updatedBook, tokenData.token)
        .then(() => dispatch(updateBookAction(updatedBook)))
        .catch((err) => alert(err));
    }, [tokenData.token]
  );

  useEffect(fetchData, [tokenData]);

  return tokenData ? (
    <>
      <ul>
        {books?.map((b) => (
          <BooksListItem key={b.id} book={b} remove={removeBook} setUpdateId={setUpdateBookId} />
        ))}
      </ul>
      <AddBook add={addBook} />
      {updateBookId && (
        <UpdateBook
          bookToUpdate={books.find((b) => b.id === updateBookId)}
          update={updateBook}
        />)}
    </>
  ) : (
    <span>No token</span>
  );
}
