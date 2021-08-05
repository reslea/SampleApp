import React from 'react';
import { Formik, Field } from 'formik';
import { Form, Container, Button } from 'react-bootstrap';
import './AppCheckout.css';
import CitySelect from './CitySelect';
import DeliveryTypeRadio from './DeliveryTypeRadio';
import DeliveryInfo from './DeliveryInfo';
import DeliveryIntervalRadio from './DeliveryIntervalRadio';

const initialValues = {
  contact: {
    firstName: 'Steve',
    lastName: 'Jobs',
    phone: +380999726478,
    city: 'Kherson',
  },
  delivery: {
    type: 'store',
    address: {
      street: 'Puhacheva',
      building: 14,
      flat: 10
    },
    timeInterval: ''
  },
  payment: {

  }
};

function submit(data) {
  console.log('submitted', JSON.stringify(data, null, 2));
}

export default function AppCheckout() {
  return (
    <Formik
    initialValues={initialValues}
    onSubmit={submit}
    >
      {({ values, handleSubmit }) => (
        <Container>
          <form onSubmit={handleSubmit}>
            <h3>Contact</h3>
            <Field name='contact.firstName' as={Form.Control} />
            <Field name='contact.lastName' as={Form.Control} />
            <Field name='contact.phone' as={Form.Control} />
            <Field name='contact.city' as={CitySelect} />

            <h3>Delivery</h3>
            <Field name='delivery.type' as={DeliveryTypeRadio} />
            <Field name='delivery.details' as={DeliveryInfo} deliveryType={values.delivery.type} />
            <Field name='delivery.timeInterval' as={DeliveryIntervalRadio} />
            <Button
               type='submit'
              variant='primary'>Submit</Button>
          </form>
          <pre>{JSON.stringify(values, null, 2)}</pre>
        </Container>
      )}
    </Formik>
  )
}
