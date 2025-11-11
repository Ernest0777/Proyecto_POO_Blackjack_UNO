using System;

namespace BlackJack_1.Utilidades;

public static class Randomizador
{
    private static readonly Random generadorAleatorio = new();

    public static List<TipoElemento> BarajarLista<TipoElemento>(List<TipoElemento> listaOriginal)
    {
        var listaMezclada = new List<TipoElemento>(listaOriginal);
        int cantidadElementos = listaMezclada.Count;
            while (cantidadElementos > 1)
            {
            cantidadElementos--;
            int indiceAleatorio = generadorAleatorio.Next(cantidadElementos + 1);
            (listaMezclada[indiceAleatorio], listaMezclada[cantidadElementos]) =
                (listaMezclada[cantidadElementos], listaMezclada[indiceAleatorio]);
            }

        return listaMezclada;
    }
}
