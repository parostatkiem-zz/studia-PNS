const elems = {
  container: null,
  brush: null,
  width: null,
  height: null
};
let currentType = 0;

const square_colors = ["#3b873b", "#fc5", "#08a"];

const DrawSquares = () => {
  const width = elems.width.value;
  const height = elems.height.value;

  while (elems.container.firstChild) {
    elems.container.removeChild(elems.container.firstChild);
  }

  const w = window.getComputedStyle(elems.container).width;
  elems.container.style.gridTemplateColumns = `repeat(${width}, 1fr)`;
  elems.container.style.gridTemplateRows = `repeat(${height}, 1fr)`;
  elems.container.style.height = `calc(${height / width} * ${w})`;

  for (let y = 0; y < width; y++) {
    for (let x = 0; x < height; x++) {
      let square = document.createElement("div");
      square.classList.add("square");
      square.title = "0";

      square.onclick = () => HandleSquareClick(event);
      elems.container.appendChild(square);
    }
  }
};

window.onload = () => {
  elems.container = document.querySelector("#container");
  elems.brush = document.querySelector("#brush");
  elems.width = document.querySelector('input[name="width"]');
  elems.height = document.querySelector('input[name="height"]');
};

const HandleSquareClick = e => {
  e.target.title = currentType;
};
const HandleTypeChange = e => {
  currentType = e.target.value;
  elems.brush.style.fill = square_colors[currentType];
  return false;
};

const HandleContainerHover = e => {
  elems.brush.style.display = "block";
  return false;
};

const HandleContainerLeave = e => {
  elems.brush.style.display = "none";
  return false;
};

const HandleContainerMove = e => {
  setTimeout(() => {
    elems.brush.style.left = e.clientX + 5 + "px";
    elems.brush.style.top = `calc(${e.clientY}px - 5px - 2em)`;
  });

  return false;
};

const HandleMapParamsChange = async () => {
  if (confirm("Czy na pewno? STRACISZ WSZYSTKIE DANE!!")) {
    await DrawSquares();
  }
  return false;
};
