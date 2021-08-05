import React from 'react';
import { Form } from 'react-bootstrap';

const cities = ['Kherson', 'Odessa', 'Kyiv'];

export default function CitySelect({ name, value, onChange }) {
  return (
    <Form.Control as='select' name={name} value={value} onChange={onChange}>
      <>
        <option key={-1} value=''></option>
        {cities.map((city, idx) => (
          <option key={idx} value={city}>{city}</option>
        ))}
      </>
    </Form.Control>
  )
}
