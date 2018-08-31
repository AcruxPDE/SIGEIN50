/*
  
  Archivo para dibujar en una aplicación Web.
  Referencia: http://www.davidbetz.net/graphics/
  
  Cómo se utiliza:
  
  1) Ejecutar InitGraphContext() en el evento onload de <body>
  
  2) En el cuerpo, poner la siguiente etiqueta HTML:
      
      <body onload="InitGraphContext();">
  
  3) Crear un <div> como contenedor como primera etiqueta funcional HTML:
  
      <body onload="InitGraphContext();">
          <form id="form1" runat="server">
              <div id="dynamicAttacher"></div>  <<<---- Esta es la etiqueta a poner
              ...

  4) Crear un estilo nuevo, Ink, el que puede estar en un archivo .css o en la sección <styles> de <head>:
  
      <head> 
          ...
          <style type="text/css">
          .Ink 
          {
              position: absolute;
              background-color: #fff;
              border-top: 1px solid transparent;
              width: 1px;
              height: 1px;
          }        
          </style>            
      </head>
      
      *** Para este proyecto, lo incluí en websyles.css
      
  */

var dynamicAttacherObj;

function InitGraphContext() {
    dynamicAttacherObj = document.getElementById('dynamicAttacher');
}

// Dibuja un pixel en la posición indicada
function PlotPixel(x, y, c) {
    var pixel = document.createElement('div');
    pixel.className = 'Ink';
    pixel.style.borderTopColor = c;
    pixel.style.left = x + 'px';
    pixel.style.top = y + 'px';
    dynamicAttacherObj.appendChild(pixel);
}

// Dibuja una línea horizontal
function DrawHorizontalLine(x, y, l, c) {
    var longPixel = document.createElement('div');
    longPixel.className = 'Ink';
    longPixel.style.borderTopColor = c;
    longPixel.style.width = l + 'px';
    longPixel.style.left = x + 'px';
    longPixel.style.top = y + 'px';
    dynamicAttacherObj.appendChild(longPixel);
}

// Dibuja una línea vertical
function DrawVerticalLine(x, y, l, c) {
    var longPixel = document.createElement('div');
    longPixel.className = 'Ink';
    longPixel.style.border = '0';
    longPixel.style.backgroundColor = c;
    longPixel.style.height = l + 'px';
    longPixel.style.left = x + 'px';
    longPixel.style.top = y + 'px';
    dynamicAttacherObj.appendChild(longPixel);
}

// Dibuja una linea de las coordenadas a las coordenadas
function DrawLine(x1, y1, x2, y2, c) {
    var steep = Math.abs(y2 - y1) > Math.abs(x2 - x1);
    if (steep) {
        t = y1;
        y1 = x1;
        x1 = t;
        t = y2;
        y2 = x2;
        x2 = t;
    }
    var deltaX = Math.abs(x2 - x1);
    var deltaY = Math.abs(y2 - y1);
    var error = 0;
    var deltaErr = deltaY;
    var xStep;
    var yStep;
    var x = x1;
    var y = y1;

    if (x1 < x2)
        xStep = 1;
    else
        xStep = -1;

    if (y1 < y2)
        yStep = 1;
    else
        yStep = -1;

    if (steep)
        PlotPixel(y, x, c);
    else
        PlotPixel(x, y, c);

    while (x != x2) {
        x += xStep;
        error += deltaErr;
        if (2 * error >= deltaX) {
            y += yStep;
            error -= deltaX;
        }
        if (steep)
            PlotPixel(y, x, c);
        else
            PlotPixel(x, y, c);

    }
}

// Dibuja un círculo
function DrawCircle(xc, yc, r, c) {
    var x = xc - r;
    var y = yc - r;

    yc = yc - r / 2;
    xc = xc - r;

    dynamicAttacherObj.style.left = x + 'px';
    dynamicAttacherObj.style.top = y + 'px';
    dynamicAttacherObj.style.width = r * 2 + 'px';
    dynamicAttacherObj.style.height = r * 2 + 'px';

    var r2 = r * r;
    x = 1;
    y = parseInt((Math.sqrt(r2 - 1) + 0.5));

    PlotPixel(xc, yc + r, c);
    PlotPixel(xc, yc - r, c);
    PlotPixel(xc + r, yc, c);
    PlotPixel(xc - r, yc, c);

    while (x < y) {
        PlotPixel(xc + x, yc + y, c);
        PlotPixel(xc + x, yc - y, c);
        PlotPixel(xc - x, yc + y, c);
        PlotPixel(xc - x, yc - y, c);
        PlotPixel(xc + y, yc + x, c);
        PlotPixel(xc + y, yc - x, c);
        PlotPixel(xc - y, yc + x, c);
        PlotPixel(xc - y, yc - x, c);

        x += 1;
        y = parseInt((Math.sqrt(r2 - x * x) + 0.5));
    }

    if (x == y) {
        PlotPixel(xc + x, yc + y, c);
        PlotPixel(xc + x, yc - y, c);
        PlotPixel(xc - x, yc + y, c);
        PlotPixel(xc - x, yc - y, c);
    }
}

/* Esta función supuestamente es chipocludisima, pero nomás no la hago funcionar 

function CreateCanvas(w, h, id) 
{
    var canvas = document.createElement('div');
    canvas.className = 'Absolute';
    canvas.id = id;
    canvas.style.width = w + 'px';
    canvas.style.height = h + 'px';

    canvas.PlotPixel = function(x, y, c, id) 
    {
        var pixel = document.createElement('div');
        pixel.className = 'Ink';
        pixel.id = id;
        pixel.style.borderTopColor = c;
        pixel.style.left = x + 'px';
        pixel.style.top = y + 'px';

        if (x <= parseInt(canvas.style.width) && y <= parseInt(canvas.style.height)) 
        {   this.firstChild.appendChild(pixel); }

    }

    canvas.SetLocation = function(x, y) 
    {
        canvas.style.left = x + 'px';
        canvas.style.top = y + 'px';
    }

    canvas.CreateBorder = function(width, style, color) 
    {
        this.style.border = width + ' ' + style + ' ' + color;
    }

    canvas.CreateContainer = function(w, h, id) 
    {
        var container = document.createElement('div');
        container.className = 'Absolute';
        container.id = id + 'Container';
        container.style.width = parseInt(w) + 'px';
        container.style.height = parseInt(h) + 'px';
        return container;
    }

    canvas.CleanCanvas = function() 
    {
        if (this.firstChild) 
        {   this.removeChild(this.firstChild); }
        this.appendChild(this.CreateCanvasContainer(this.style.width, this.style.height, this.id));
    }

    //canvas.appendChild(this.CreateContainer(w, h, id));
    canvas.appendChild(dynamicAttacherObj.CreateContainer(w, h, id));
    return canvas;
}

function AttachObject(parentObj, childObj) 
{
    parentObj.appendChild(childObj);
}

*/