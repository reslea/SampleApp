const actions = {
  getAll: 'getAll',
  getOne: 'getOne',
  add: 'add',
  update: 'update',
  remove: 'remove',
}

export const initialState = {
  books: []
};

  export function booksReducer(state, { type, payload }) {
  switch(type) {
    case actions.getAll:
      const { books } = payload;
      return {
        ...state,
        books,
      };
    case actions.add:
      const { newBook } = payload;
      return {
        ...state,
        books: [...state.books, newBook]
      };
    case actions.update:
      const { updatedBook } = payload;
      return {
        ...state,
        books: state.books.map(b => b.id === updatedBook.id
          ? updatedBook
          : b)
      };
    case actions.remove:
      const { id } = payload;

      return {
        ...state,
        books: state.books.filter(b => b.id !== id)
      };
    default:
      return state;
  }
}

export const getBooksAction = books => ({
  type: actions.getAll,
  payload: { books }
});

export const addBookAction = newBook => ({
  type: actions.add,
  payload: { newBook }
});

export const updateBookAction = updatedBook => ({
  type: actions.update,
  payload: { updatedBook }
})

export const removeBookAction = id => ({
  type: actions.remove,
  payload: { id }
});
