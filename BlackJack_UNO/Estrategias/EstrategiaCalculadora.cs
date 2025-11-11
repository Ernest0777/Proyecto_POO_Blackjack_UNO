namespace BlackJack_1.Estrategias;

using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Uno;
using BlackJack_1.Utilidades;

public class EstrategiaCalculadora : IEstrategiaJugadorUno
{
    public string NombreEstrategia => "Calculadora";

    public string UltimaAccion { get; private set; } = "Analizando situación";

    // Decide que carta jugar segun las circunstancias
    public CartaUno? DecidirCarta(
        JugadorUno jugadorActual,
        CartaUno cartaSuperior,
        JugadorUno? siguienteJugador = null,
        MazoUno? mazo = null)
    {
        // Obtiene las cartas que puede jugar en este turno
        var cartasJugables = jugadorActual.ObtenerMano()
            .OfType<CartaUno>()
            .Where(carta => ReglasUno.PuedeJugar(cartaSuperior, carta))
            .ToList();

        // Si no puede jugar ninguna carta cone una (null)
        if (cartasJugables.Count == 0)
        {
            UltimaAccion = "No tiene cartas validas, toma una carta";
            return null;
        }

        // Si solo tiene un +4 y el siguiente jugador tiene 3 o menos cartas guarda el +4 (preparandose para el impacto)
        var tieneSoloMasCuatro = cartasJugables.Count == 1 && cartasJugables[0].Tipo == TipoCartaUno.MasCuatro;
        if (tieneSoloMasCuatro && siguienteJugador?.ObtenerMano().Count() <= 3)
        {
            var cartaTomada = mazo?.SacarCarta();
            if (cartaTomada != null)
                jugadorActual.AgregarCarta(cartaTomada);

            UltimaAccion = "Guarda su +4 y toma una carta esperando mejor momento(RATA)";
            return null;
        }

        // Si el siguiente jugador tiene 1 carta pone cartas ofensivas priorizando las que lo hacen comer
        if (siguienteJugador?.ObtenerMano().Count() == 1)
        {
            var cartaMasCuatro = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.MasCuatro);
            if (cartaMasCuatro != null)
            {
                UltimaAccion = $"Usa {cartaMasCuatro} para que {siguienteJugador.Nombre} lo odie toda su vida";
                return cartaMasCuatro;
            }

            var cartaMasDos = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.MasDos);
            if (cartaMasDos != null)
            {
                UltimaAccion = $"Usa {cartaMasDos} para arruinarle la diversion a {siguienteJugador.Nombre}";
                return cartaMasDos;
            }

            var cartaBloqueo = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Bloqueo);
            if (cartaBloqueo != null)
            {
                UltimaAccion = $"Usa {cartaBloqueo} para saltar a {siguienteJugador.Nombre}";
                return cartaBloqueo;
            }

            var cartaReversa = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Reversa);
            if (cartaReversa != null)
            {
                UltimaAccion = $"Invierte el sentido con {cartaReversa}";
                return cartaReversa;
            }

            // Si no tiene cartas ofensivas, roba una carta buscando un comodín
            var cartaNueva = mazo?.SacarCarta();
            if (cartaNueva != null)
                jugadorActual.AgregarCarta(cartaNueva);

            UltimaAccion = "No tiene cartas especiales roba buscando un comodin";
            return null;
        }

        // Si no hay riesgo juega una carta normal
        var cartaNormal = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Normal);
        if (cartaNormal != null)
        {
            UltimaAccion = $"Juega carta {cartaNormal}";
            return cartaNormal;
        }

        // Si no tiene normales intenta usar cartas no ofensivas primero
        var cartaReversa = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Reversa);
        if (cartaReversa != null)
        {
            UltimaAccion = $"Usa {cartaReversa} para mantener el flujo";
            return cartaReversa;
        }

        var cartaBloqueoNeutra = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.Bloqueo);
        if (cartaBloqueoNeutra != null)
        {
            UltimaAccion = $"Usa {cartaBloqueoNeutra} sin intencion de molestar";
            return cartaBloqueoNeutra;
        }

        // Si no tiene nada mas usa un +4 o +2
        var cartasMasCuatro = cartasJugables.Where(c => c.Tipo == TipoCartaUno.MasCuatro).ToList();
        if (cartasMasCuatro.Any())
        {
            // Si tiene más de un +4 puede jugar uno sin miedo
            if (cartasMasCuatro.Count > 1)
            {
                var cartaMasCuatro = cartasMasCuatro.First();
                UltimaAccion = $"Usa {cartaMasCuatro} para avanzar";
                return cartaMasCuatro;
            }

            // Si solo tiene un +4 y otras cartas validas puede usarlo (buen uso)
            if (cartasJugables.Count > 1)
            {
                var cartaMasCuatro = cartasMasCuatro.First();
                UltimaAccion = $"Usa {cartaMasCuatro} estrategicamente";
                return cartaMasCuatro;
            }

            // Si su unico +4 es la unica carta jugable prefiere comer
            var cartaTomada = mazo?.SacarCarta();
            if (cartaTomada != null)
                jugadorActual.AgregarCarta(cartaTomada);

            UltimaAccion = "Tiene un solo +4 jugable sin embargo prefiere comer una carta";
            return null;
        }

        // Si no tiene +4 busca un +2 como ultima opcion ofensiva
        var cartaMasDos = cartasJugables.FirstOrDefault(c => c.Tipo == TipoCartaUno.MasDos);
        if (cartaMasDos != null)
        {
            UltimaAccion = $"Usa {cartaMasDos} para limpiar el mazo";
            return cartaMasDos;
        }

        // Si no tiene ninguna de las anteriores juega una cualquiera valida.
        var cartaAleatoria = cartasJugables[new Random().Next(cartasJugables.Count)];
        UltimaAccion = $"Juega {cartaAleatoria} porque no hay puede hacer mas";
        return cartaAleatoria;
    }
}
