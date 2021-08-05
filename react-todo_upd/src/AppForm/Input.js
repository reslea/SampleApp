import React from 'react';
import './Input.css';
import { Form, InputGroup } from 'react-bootstrap';

function Input({ name, value, onChange, invalid, validationMessage }) {
  return (
  <>
    <Form.Group>
      <InputGroup hasValidation>
        <Form.Control name={name} value={value} onChange={onChange} isInvalid={invalid} />
        <Form.Control.Feedback type={invalid ? 'invalid' : ''}>
          {validationMessage}
        </Form.Control.Feedback>
      </InputGroup>
    </Form.Group>
  </>
  )
}

export default Input;