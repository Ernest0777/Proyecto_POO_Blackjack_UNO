namespace BlackJack_1.Estrategias;

using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Uno;
using BlackJack_1.Utilidades;

public class EstrategiaAleatoria : IEstrategiaJugadorUno
{
    private readonly Random generadorAleatorio = new();

    public string NombreEstrategia => "Aleatoria";

    
    public string UltimaAccion { get; private set; } = "Esperando turno";

    // Decide que carta jugar al azar entre las validas
    public CartaUno? DecidirCarta(
        JugadorUno jugador,
        CartaUno cartaSuperior,
        JugadorUno? siguienteJugador = null,
        MazoUno? mazo = null)
    {
        // Filtra las cartas validas
        var cartasJugables = jugador.ObtenerMano()
            .OfType<CartaUno>()
            .Where(carta => ReglasUno.PuedeJugar(cartaSuperior, carta))
            .ToList();

        // Si no tiene ninguna carta valida come una
        if (cartasJugables.Count == 0)
        {
            UltimaAccion = "Comio una carta";
            return null;
        }

        // Elige una carta al azar entre las disponibles
        var cartaSeleccionada = cartasJugables[generadorAleatorio.Next(cartasJugables.Count)];

        UltimaAccion = $"Juega {cartaSeleccionada}";

        // Devuelve la carta seleccionada
        return cartaSeleccionada;
    }
}
