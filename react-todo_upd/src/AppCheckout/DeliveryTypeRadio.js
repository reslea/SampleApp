import React from 'react';
import { Form } from 'react-bootstrap';

const deliveryTypes = ['store', 'courier', 'nova_poshta'];

export default function DeliveryTypeRadio({ name, value, onChange }) {
  return (
    <>
      {deliveryTypes.map((deliveryType, idx) => 
        <Form.Check
          key={idx}
          type='radio'
          name={name}
          id={`delivery-radio-${idx}`}
          value={deliveryType}
          onChange={(e) => {console.log('changed'); onChange(e);}}          
          label={deliveryType}
        />
      )}
    </>
  )
}
