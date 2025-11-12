namespace BlackJack_1.Juegos.Uno;

using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_1.Utilidades;

public static class ReglasUno
{
    // Verifica si una carta puede jugarse sobre la carta actual en la pila de descarte
    public static bool PuedeJugar(CartaUno cartaSuperior, CartaUno cartaJugada)
    {

        return cartaJugada.Color == cartaSuperior.Color
            || cartaJugada.Valor == cartaSuperior.Valor
            || cartaJugada.Tipo == cartaSuperior.Tipo
            || cartaJugada.Color == "Negro";
    }

    // Aplica los efectos de una carta especial
    public static void AplicarEfecto(
        CartaUno cartaJugada,
        ref int direccionTurnos,
        Queue<JugadorUno> colaJugadores,
        MazoUno mazo,
        JugadorUno jugadorActual)
    {
        switch (cartaJugada.Tipo)
        {
            case TipoCartaUno.MasDos:
                // El siguiente jugador toma 2 cartas
                if (colaJugadores.Count > 0)
                {
                    var siguiente = colaJugadores.Peek();
                    for (int i = 0; i < 2; i++)
                        siguiente.AgregarCarta(mazo.SacarCarta());

                    ConsolaLogger.Mostrar($"{siguiente.Nombre} toma 2 cartas (+2).");
                }
                break;

            case TipoCartaUno.MasCuatro:
                // Siguiente jugador toma 4 cartas
                if (colaJugadores.Count > 0)
                {
                    var siguiente = colaJugadores.Peek();
                    for (int i = 0; i < 4; i++)
                        siguiente.AgregarCarta(mazo.SacarCarta());

                    ConsolaLogger.Mostrar($"{siguiente.Nombre} toma 4 cartas (+4).");
                }

                CambiarColorInteligente(jugadorActual);
                break;

            case TipoCartaUno.Bloqueo:
                ConsolaLogger.Mostrar("El siguiente jugador pierde su turno (Bloqueo).");
                break;

            case TipoCartaUno.Reversa:
                direccionTurnos *= -1;
                ConsolaLogger.Mostrar("<-Cambio de sentido->");
                break;

            case TipoCartaUno.CambioColor:
                CambiarColorInteligente(jugadorActual);
                break;
        }
    }

    private static void CambiarColorInteligente(JugadorUno jugadorActual)
{
    // Agrupa las cartas por color (exceptuando los comodines negros)
    var gruposPorColor = jugadorActual.ObtenerMano()
        .Where(c => c.Color != "Negro")
        .GroupBy(c => c.Color)
        .Select(g => new { Color = g.Key, Cantidad = g.Count() })
        .ToList();

    string colorElegido;

    if (gruposPorColor.Count == 0)
    {
        // Si solo tiene comodines elige al azar
        string[] coloresDisponibles = { "Rojo", "Azul", "Verde", "Amarillo" };
        colorElegido = coloresDisponibles[new Random().Next(coloresDisponibles.Length)];
        ConsolaLogger.Mostrar($"{jugadorActual.Nombre} no tiene cartas de color, cambia al azar a {colorElegido}.");
    }
    else
    {
        // Encuentra el maximo numero de cartas de un color
        int maxCantidad = gruposPorColor.Max(g => g.Cantidad);

        var coloresMaximos = gruposPorColor
            .Where(g => g.Cantidad == maxCantidad)
            .Select(g => g.Color)
            .ToList();

        // Si hay empate, elige al azar entre los colores con mayor cantidad
        if (coloresMaximos.Count > 1)
            colorElegido = coloresMaximos[new Random().Next(coloresMaximos.Count)];
        else
            colorElegido = coloresMaximos.First();

        ConsolaLogger.Mostrar($"{jugadorActual.Nombre} cambia el color a {colorElegido} (elige el color donde tiene más cartas).");
    }
}


    // Verifica si el jugador no tiene cartas (condición de victoria)
    public static bool HaGanado(JugadorUno jugador)
    {
        return !jugador.ObtenerMano().Any();
    }
}
