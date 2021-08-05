import React, { useCallback, useState, useEffect } from 'react';
import Input from './Input';
import { Form, Button, Container, Col } from 'react-bootstrap';

export default function PersonForm({ }) {   
  const [isSubmitting, setIsSubmitting] = useState(false);
  
  const [person, setPerson] = useState({
    firstName: 'Bob',
    lastName: 'Dilan',
    age: 40,
  });
  const [personErrors, setPersonErrors] = useState({ });

  const onSubmit = useCallback((e) => {
    e.preventDefault();
    setIsSubmitting(true);

    // request here

    setIsSubmitting(false);
  });

  function validate(person) {
  }

  useEffect(() => {
    setPersonErrors(validate(person));
  }, [person]);

  function handleChange(e) {
    e.persist();
    const name = e.target.name;
    setPerson(prev => ({
      ...prev, 
      [name]: e.target.value
    }));
  }

  return (    
    <Container>
      <Col md={6}>
        <Form onSubmit={onSubmit}>
          <div>
            <Input 
              name='firstName' 
              value={person.firstName}
              onChange={handleChange}
              invalid={personErrors.firstName}
              validationMessage={personErrors.firstName}
            />
          </div>
          <div>
            <Input 
              name='lastName' 
              value={person.lastName}
              onChange={handleChange} 
            />
          </div>
          <div>
            <Input 
              name='age' 
              value={person.age}
              onChange={handleChange} 
            />
          </div>
          <Button type='submit' disabled={isSubmitting}>Submit</Button>
        </Form>
        <span>{JSON.stringify(person)}</span>
      </Col>
    </Container>
  )
}