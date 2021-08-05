import React, { useState, useMemo} from 'react';
import './App.css';

function displayAlert() {
  alert('hello');
}

function MemoApp() {
  const [x, setX] = useState(0);
  const [y, setY] = useState(0);

  return (
    <>
      <A />
      <B x={x} setX={setX} name={'x'} color={'red'} />
      <B x={y} setX={setY} name={'y'} />
    </>
  );
}

const A = React.memo(function () {

  console.log('A render');

  return <div>A</div>;
});

const B = React.memo(function( { x, setX, name, color } ) {
  console.log(`${name} render`);

  const style = useMemo(() => ({ color }), [color]);

  return (
  <>
    <div style={style}>{x}</div>
    <button onClick={() => setX(x => x+1)}>increment</button>
  </>
  )
});