namespace BlackJack_1.Estrategias;

using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Uno;
using BlackJack_1.Utilidades;


public class EstrategiaCalculadora : IEstrategiaJugador
{
    
    // Decide que carta jugar según la situación del juego y las cartas del siguiente jugador
    
    public CartaUno? DecidirCarta(JugadorUno jugadorActual, CartaUno cartaSuperior, JugadorUno siguienteJugador, MazoUno mazo)
    {
        var cartasJugables = jugadorActual.ObtenerMano()
            .OfType<CartaUno>()
            .Where(carta => ReglasUno.PuedeJugar(cartaSuperior, carta))
            .ToList();

        if (cartasJugables.Count == 0)
            return null;

        // caso especial tiene solo un +4 y el siguiente jugador tiene 3 o menos cartas
        var tieneSoloMasCuatro = cartasJugables.Count == 1 && cartasJugables[0].Tipo == TipoCartaUno.MasCuatro;
        if (tieneSoloMasCuatro && siguienteJugador.ObtenerMano().Count() <= 3)
        {
            // Prefiere guardarlo y tomar carta
            var cartaTomada = mazo.SacarCarta();
            jugadorActual.AgregarCarta(cartaTomada);
            ConsolaLogger.Mostrar($"{jugadorActual.Nombre} decide guardar su +4 y toma una carta ({cartaTomada}) esperando mejor momento, esperando a que su presa baje la guardia");
            return null;
        }

        // el siguiente jugador tiene solo una carta (rata)
        if (siguienteJugador.ObtenerMano().Count() == 1)
        {
            var cartaMasCuatro = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.MasCuatro);
            if (cartaMasCuatro != null)
            {
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} lanza {cartaMasCuatro} para frenar a {siguienteJugador.Nombre}");
                return cartaMasCuatro;
            }

            var cartaMasDos = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.MasDos);
            if (cartaMasDos != null)
            {
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} lanza {cartaMasDos} para chingar a {siguienteJugador.Nombre}");
                return cartaMasDos;
            }

            var cartaBloqueo = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Bloqueo);
            if (cartaBloqueo != null)
            {
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} usa {cartaBloqueo} para saltar el turno de {siguienteJugador.Nombre}");
                return cartaBloqueo;
            }

            var cartaReversa = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Reversa);
            if (cartaReversa != null)
            {
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} usa {cartaReversa} para invertir el orden y evitar que {siguienteJugador.Nombre} gane");
                return cartaReversa;
            }

            // Si no tiene cartas comodines que hagan comer toma una esperando a que le salga una (RATA)
            var cartaTomada = mazo.SacarCarta();
            jugadorActual.AgregarCarta(cartaTomada);
            ConsolaLogger.Mostrar($"{jugadorActual.Nombre} no tiene cartas especiales toma una ({cartaTomada}) esperando un comodin(BIEN RATA)");
            return null;
        }

        // juega una carta normal pues no esta modo rata
        var cartaNormal = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Normal);
        if (cartaNormal != null)
        {
            ConsolaLogger.Mostrar($"{jugadorActual.Nombre} juega una carta normal ({cartaNormal}).");
            return cartaNormal;
        }

        // no tiene cartas normales, lanza cualquier carta válida
        var cartaAleatoria = cartasJugables[new Random().Next(cartasJugables.Count)];
        ConsolaLogger.Mostrar($"{jugadorActual.Nombre} juega {cartaAleatoria} (sin riesgo).");
        return cartaAleatoria;
    }
}
