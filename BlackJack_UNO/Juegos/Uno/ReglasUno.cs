namespace BlackJack_1.Juegos.Uno;

using BlackJack_1.Utilidades;

public static class ReglasUno
{
    
    // Verifica si una carta puede jugarse sobre la carta actual en la pila de descarte.
    
    public static bool PuedeJugar(CartaUno cartaActual, CartaUno cartaJugada)
    {
        // Se puede jugar si:
        // -Es del mismo color,numero,tipo o comodin(negra)
        return cartaJugada.Color == cartaActual.Color
            || cartaJugada.Numero == cartaActual.Numero
            || cartaJugada.Tipo == cartaActual.Tipo
            || cartaJugada.Color == ColorCartaUno.Negro;
    }

    
   
    
    public static void AplicarEfecto(CartaUno cartaJugada, ref int direccionTurnos, Queue<JugadorUno> colaJugadores, MazoUno mazo, JugadorUno jugadorActual)
    {
        switch (cartaJugada.Tipo)
        {
            case TipoCartaUno.MasDos:
                // El siguiente jugador toma 2 cartas
                if (colaJugadores.Count > 0)
                {
                    var siguiente = colaJugadores.Peek();
                    for (int cartaExtra = 0; cartaExtra < 2; cartaExtra++)
                        siguiente.AgregarCarta(mazo.SacarCarta());

                    ConsolaLogger.Mostrar($"{siguiente.Nombre} toma 2 cartas (+2).");
                }
                break;

            case TipoCartaUno.MasCuatro:
                // Siguiente jugador toma 4 cartas y cambia color
                if (colaJugadores.Count > 0)
                {
                    var siguiente = colaJugadores.Peek();
                    for (int cartaExtra = 0; cartaExtra < 4; cartaExtra++)
                        siguiente.AgregarCarta(mazo.SacarCarta());

                    ConsolaLogger.Mostrar($"{siguiente.Nombre} toma 4 cartas (+4).");
                }
                break;

            case TipoCartaUno.Bloqueo:
                // Salta al siguiente jugador 
                ConsolaLogger.Mostrar("El siguiente jugador pierde su turno (Bloqueo).");
                break;

            case TipoCartaUno.Reversa:
                // Invierte el sentido de los turnos
                direccionTurnos *= -1;
                ConsolaLogger.Mostrar("¡Cambio de sentido!");
                break;

            case TipoCartaUno.CambioColor:
                // El jugador actual elige un color al azar 
                var coloresDisponibles = Enum.GetValues<ColorCartaUno>().Where(c => c != ColorCartaUno.Negro).ToArray();
                var colorElegido = coloresDisponibles[new Random().Next(coloresDisponibles.Length)];
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} cambia el color a {colorElegido}.");
                break;
        }
    }

    
    // verifica si el jugador tiene cartas (para ganar)
    
    public static bool HaGanado(JugadorUno jugador)
    {
        return !jugador.ObtenerMano().Any();
    }
}
