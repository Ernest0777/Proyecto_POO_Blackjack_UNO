namespace BlackJack_1.Juegos.Uno;

using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;
using BlackJack_1.Utilidades;

public class JugadorUno : Jugador
{
    public JugadorUno(string nombreJugador, IEstrategiaJugador estrategia)
        : base(nombreJugador, estrategia)
    {
    }

    
    // Ejecuta el turno del jugador segun la estrategia
    
    public override void TomarTurno()
    {
        var cartaElegida = Estrategia.DecidirCarta(this);

        if (cartaElegida != null)
        {
            Mano.Remove(cartaElegida);
            ConsolaLogger.Mostrar($"{Nombre} juega {cartaElegida}");

            if (Mano.Count == 1)
                ConsolaLogger.Mostrar($"{Nombre} grita: ¡UNO!");
        }
        else
        {
            ConsolaLogger.Mostrar($"{Nombre} no puede jugar, toma una carta del mazo.");
        }
    }
}
