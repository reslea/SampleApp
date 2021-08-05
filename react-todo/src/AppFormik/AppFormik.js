import React from 'react';
import { Formik } from 'formik';

const initialFormValues = {
  firstName: 'Bob',
  lastName: 'Dilan',
  age: 40,
};

function validate(person) {
  const errors = { };
  if(!person.firstName) {
    errors.firstName = 'this field is required';
  }

  return errors;
}

function submit(person, { setSubmitting }) {
  setSubmitting(true);
  console.log('submitted', JSON.stringify(person));
  setSubmitting(false);
}

export default function AppFormik() {
  return (
  <Formik
    initialValues={initialFormValues}
    validate={validate}
    submit={submit}
  >
    {({ values, handleChange, handleSubmit }) => (
      <>
        <form onSubmit={handleSubmit}>
          <input name='firstName' value={values.firstName} onChange={handleChange} />
        </form>      
        <span>{JSON.stringify(values)}</span>
      </>
    )}
  </Formik>)
}

