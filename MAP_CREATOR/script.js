const elems = {
  container: null,
  width: null,
  height: null,
};
let currentType = 0;

const DrawSquares = () => {
  const width = elems.width.value;
  const height = elems.height.value;
  while (elems.container.firstChild) {
    elems.container.removeChild(elems.container.firstChild);
  }

  elems.container.style.gridTemplateColumns = `repeat(${width}, 1fr)`;
  elems.container.style.gridTemplateRows = `repeat(${height}, 1fr)`;

  for (let y = 0; y < width; y++) {
    for (let x = 0; x < height; x++) {
      let square = document.createElement('div');

      square.classList.add('square');
      square.title = '0';
      // square.style.width = `calc((95vw - ${width} * 0.2em) / ${width})`;

      //  square.style.height = square.style.width;
      square.onclick = () => HandleSquareClick(event);
      elems.container.appendChild(square);
    }
    let br = document.createElement('br');
    //elems.container.appendChild(br);
  }
};

window.onload = () => {
  elems.container = document.querySelector('#container');
  elems.width = document.querySelector('input[name="width"]');
  elems.height = document.querySelector('input[name="height"]');
};

const HandleSquareClick = e => {
  console.dir(e.target);
  e.target.title = currentType;
};
const HandleTypeChange = e => {
  console.log(e.target.value);
  currentType = e.target.value;
  return false;
};

const HandleMapParamsChange = async () => {
  if (confirm('Czy na pewno? STRACISZ WSZYSTKIE DANE!!')) {
    await DrawSquares();
  }
  return false;
};
