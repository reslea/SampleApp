import React from 'react';
import { Row, Col, Form } from 'react-bootstrap';
import './DeliveryIntervalRadio.css';

const deliveryIntervalOptions = [{
  intervalId: 1002,
  date: '18 July',
  interval: '11:00 - 18:00',
},{
  intervalId: 1003,
  date: '19 July',
  interval: '11:00 - 19:00',
},{
  intervalId: 1004,
  date: '20 July',
  interval: '11:00 - 20:00',
},{
  intervalId: 1005,
  date: '21 July',
  interval: '11:00 - 21:00',
},{
  intervalId: 1006,
  date: '22 July',
  interval: '11:00 - 22:00',
}];

export default function DeliveryIntervalRadio({ name, value, onChange }) {
  return (
    <Row>
    {(deliveryIntervalOptions.map((option, idx) => {
      const id=`delivery-interval-${idx}`;
      const isSelected = value === option.interval;
      return (
      <Col key={idx} className='time-interval'>
        <label htmlFor={id} className={isSelected ? 'selected' : ''}>
          <div>{option.date}</div>
          <div>{option.interval}</div>
        </label>
        <input
          type='radio'
          id={id}
          name={name}          
          value={option.interval}
          onChange={onChange}
          hidden
        />
      </Col>
    )}))}
    </Row>
  )
}
