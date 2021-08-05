import React from 'react';
import { Field } from 'formik';

export default function DeliveryInfo({ name, deliveryType, value }) {
  switch (deliveryType) {
    case 'store':
      return <Field name={`${name}.storeId`} value={1} />;
    case 'courier':
      return (
      <>
      <Field name={`${name}.address.street`} />
      <Field name={`${name}.address.building`} />
      <Field name={`${name}.address.flat`} />
      </>)
    case 'delivery_office':
      return (
      <>
        <Field name={`${name}.cityId`} />
        <Field name={`${name}.officeId`} />
      </>)
    default:
      return null;
  }
}
