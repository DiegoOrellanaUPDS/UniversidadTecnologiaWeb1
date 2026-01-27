// Sistema de Biblioteca
class Biblioteca {
    constructor() {
        this.libros = [];
        this.inicializarEventos();
    }

    inicializarEventos() {
        const form = document.getElementById('formLibro');
        if (form) {
            form.addEventListener('submit', (e) => {
                e.preventDefault();
                this.agregarLibro();
            });
        }
    }

    agregarLibro() {
        const titulo = document.getElementById('titulo').value;
        const autor = document.getElementById('autor').value;

        const libro = {
            id: Date.now(),
            titulo: titulo,
            autor: autor
        };

        this.libros.push(libro);
        this.mostrarLibros();
        document.getElementById('formLibro').reset();
    }

    mostrarLibros() {
        const lista = document.getElementById('listaLibros');
        lista.innerHTML = this.libros.map(libro => `
            <div class="libro-card">
                <h3>${libro.titulo}</h3>
                <p>Autor: ${libro.autor}</p>
            </div>
        `).join('');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new Biblioteca();
    console.log('Sistema de Biblioteca iniciado');
});

//HOLA HOLA
