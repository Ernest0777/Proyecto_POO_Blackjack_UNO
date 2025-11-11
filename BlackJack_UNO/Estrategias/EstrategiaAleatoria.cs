namespace BlackJack_1.Estrategias;

using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Uno;
using BlackJack_1.Utilidades;

// Estrategia que elige una carta tuene que ser valida al azar
public class EstrategiaAleatoria : IEstrategiaJugador
{
    private readonly Random generadorAleatorio = new();

    //elige una aleatoria que pueda colocar
    public CartaUno? DecidirCarta(JugadorUno jugador, CartaUno cartaSuperior)
    {
        // Obtener las cartas que puede jugar
        var cartasJugables = jugador.ObtenerMano()
            .OfType<CartaUno>()
            .Where(carta => ReglasUno.PuedeJugar(cartaSuperior, carta))
            .ToList();

        // Si no tiene ninguna carta valida devuelve null y toma carta
        if (cartasJugables.Count == 0)
            return null;

        // Elegir una carta al azar entre las disponibles
        int indiceAleatorio = generadorAleatorio.Next(cartasJugables.Count);
        return cartasJugables[indiceAleatorio];
    }
}
