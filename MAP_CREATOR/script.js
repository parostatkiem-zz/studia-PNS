const elems = {
  container: null,
  brush: null,
  width: null,
  height: null,
};
let currentType = 0;
let output = {};
const square_colors = ['#3b873b', '#08a', '#fc5'];

const DrawSquares = async sourceObj => {
  while (elems.container.firstChild) {
    elems.container.removeChild(elems.container.firstChild);
  }

  const w = window.getComputedStyle(elems.container).width;
  elems.container.style.gridTemplateColumns = `repeat(${sourceObj.width}, 1fr)`;
  elems.container.style.gridTemplateRows = `repeat(${sourceObj.height}, 1fr)`;
  elems.container.style.height = `calc(${sourceObj.height /
    sourceObj.width} * ${w})`;

  for (let y = 0; y < sourceObj.width; y++) {
    for (let x = 0; x < sourceObj.height; x++) {
      let square = document.createElement('div');
      const terrainType =
        sourceObj.mapElements[sourceObj.width * y + x].terrainType;
      square.classList.add('square');
      square.title = terrainType;
      //output.mapElements.push({ terrainType });
      square.onclick = event => HandleSquareClick(event, x, y, sourceObj);
      elems.container.appendChild(square);
    }
  }
};

const UpdateCode = sourceObj => {
  elems.code.value = JSON.stringify(sourceObj, undefined, 2);
  elems.code.classList.remove('error');
};

window.onload = () => {
  elems.container = document.querySelector('#container');
  elems.brush = document.querySelector('#brush');
  elems.code = document.querySelector('#code');
  elems.width = document.querySelector('input[name="width"]');
  elems.height = document.querySelector('input[name="height"]');
};

const GenerateCode = () => {
  const code = {
    width: parseInt(elems.width.value),
    height: parseInt(elems.height.value),
    mapElements: [],
  };

  for (let i = 0; i < code.width * code.height; i++) {
    code.mapElements.push({ terrainType: 0 });
  }
  return code;
};

const HandleSquareClick = (e, x, y, sourceObj) => {
  e.target.title = currentType;
  sourceObj.mapElements[sourceObj.width * y + x].terrainType = parseInt(
    currentType,
  );
  UpdateCode(sourceObj);
};
const HandleTypeChange = e => {
  currentType = e.target.value;
  elems.brush.style.fill = square_colors[currentType];
  return false;
};

const HandleContainerHover = e => {
  elems.brush.style.display = 'block';
  return false;
};

const HandleContainerLeave = e => {
  elems.brush.style.display = 'none';
  return false;
};

const HandleContainerMove = e => {
  setTimeout(() => {
    elems.brush.style.left = e.clientX + 5 + 'px';
    elems.brush.style.top = `calc(${e.clientY}px - 5px - 2em)`;
  });

  return false;
};

const HandleMapParamsChange = async () => {
  if (confirm('Czy na pewno? STRACISZ WSZYSTKIE DANE!!')) {
    const newCode = GenerateCode();
    await DrawSquares(newCode);
    UpdateCode(newCode);
  }
  return false;
};

const HandleCodeChange = async e => {
  let newCode = null;
  try {
    newCode = JSON.parse(e.target.value);
    elems.code.classList.remove('error');
  } catch (ex) {
    elems.code.classList.add('error');
    console.warn(
      'Nie udało się przetworzyć kodu na poprawny obiekt. Zły JSON :(',
    );
    return;
  }

  await DrawSquares(newCode);

  return false;
};
