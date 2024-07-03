function DibujarArbol(raiz) {
    const canvas = new fabric.Canvas('arbolCanvas');
    const espaciadoHorizontal = 200;
    const espaciadoVertical = 150;
    const origenX = canvas.width / 2;
    const origenY = 100;

    function dibujarNodo(nodo, x, y, nivel) {
        if (!nodo) return;

        const valor = nodo.Valor.split(',');
        const fecha = valor[0];
        const matricula = valor[1];
        const asistencia = valor[2];
        const colorAsistencia = asistencia.trim().toLowerCase() === 'true' ? 'green' : 'red';

        // Crear el círculo con el radio 
        const circle = new fabric.Circle({
            left: x,
            top: y,
            radius: 65,
            fill: 'white',
            stroke: 'black',
            strokeWidth: 2,
            originX: 'center',
            originY: 'center'
        });

        // Crear los textos centrados dentro del círculo
        const textFecha = new fabric.Text(`Fecha: ${fecha}`, {
            fontSize: 11,
            fontFamily: 'Arial',
            fontWeight: 'bold',
            fill: 'black',
            originX: 'center',
            originY: 'center',
            left: x,
            top: y - 30
        });
        const textMatricula = new fabric.Text(`Matricula: ${matricula}`, {
            fontSize: 11,
            fontFamily: 'Arial',
            fontWeight: 'bold',
            fill: 'black',
            originX: 'center',
            originY: 'center',
            left: x,
            top: y - 10
        });
        const textAsistenciaLabel = new fabric.Text(`Asistencia:`, {
            fontSize: 11,
            fontFamily: 'Arial',
            fontWeight: 'bold',
            fill: 'black',
            originX: 'center',
            originY: 'center',
            left: x - 20,
            top: y + 10
        });
        const textAsistencia = new fabric.Text(`${asistencia}`, {
            fontSize: 11,
            fontFamily: 'Arial',
            fontWeight: 'bold',
            fill: colorAsistencia,
            originX: 'center',
            originY: 'center',
            left: x + 35,
            top: y + 10
        });

        const textGroup = new fabric.Group([textFecha, textMatricula, textAsistenciaLabel, textAsistencia], {
            left: x,
            top: y,
            originX: 'center',
            originY: 'center'
        });

        // Agregar elementos al canvas
        canvas.add(circle);
        canvas.add(textGroup);

        // Posiciones para los hijos
        const nextY = y + espaciadoVertical;
        if (nodo.Izquierdo) {
            const nextXIzquierdo = x - espaciadoHorizontal;
            const lineIzquierda = new fabric.Line([x, y + 65, nextXIzquierdo, nextY - 65], {
                stroke: 'black',
                strokeWidth: 2
            });
            canvas.add(lineIzquierda);
            dibujarNodo(nodo.Izquierdo, nextXIzquierdo, nextY, nivel + 1);
        }

        if (nodo.Derecho) {
            const nextXDerecho = x + espaciadoHorizontal;
            const lineDerecha = new fabric.Line([x, y + 65, nextXDerecho, nextY - 65], {
                stroke: 'black',
                strokeWidth: 2
            });
            canvas.add(lineDerecha);
            dibujarNodo(nodo.Derecho, nextXDerecho, nextY, nivel + 1);
        }
    }

    // Limpiar el canvas antes de dibujar
    canvas.clear();

    // Llamar a la función para dibujar el árbol desde la raíz
    dibujarNodo(raiz, origenX, origenY, 0);
}
