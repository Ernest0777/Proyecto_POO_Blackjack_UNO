namespace BlackJack_1.Juegos.Uno;

using System.Linq;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

// Representa a un jugador del juego UNO
public class JugadorUno : JugadorBase, IJugadorUno
{
    // Estrategia especifica de UNO (aleatoria, calculadora, etc.)
    public IEstrategiaJugadorUno Estrategia { get; set; }

    // Constructor: define id, nombre y la estrategia usada por el jugador
    public JugadorUno(int id, string nombre, IEstrategiaJugadorUno estrategia)
        : base(id, nombre)
    {
        Estrategia = estrategia;
    }

    // Calcula puntos de las cartas en mano (para el final del juego o desempate)
    public override int ObtenerPuntos()
    {
        return _mano.Sum(c => c.ObtenerValorNumerico());
    }

    // Decide que acción tomar según su estrategia
    public override void TomarDecision(IJuego juegoContexto)
    {
        if (Estrategia == null)
        {
            NotificarAccion("no tiene estrategia asignada y pasa su turno.");
            return;
        }

        // En UNO la estrategia decide qué carta jugar
        if (juegoContexto is not Uno juegoUno)
        {
            NotificarAccion("el contexto de juego no es válido.");
            return;
        }

        var cartaSuperior = juegoUno.ObtenerCartaSuperior();
        var cartaSeleccionada = Estrategia.DecidirCarta(this, cartaSuperior);

        if (cartaSeleccionada != null)
            NotificarAccion($"ha decidido jugar {cartaSeleccionada}");
        else
            NotificarAccion("no puede jugar y tomara una carta.");
    }

    // Metodos auxiliares para manipular la mano
    public void AgregarCarta(CartaUno carta)
    {
        _mano.Add(carta);
    }

    public void QuitarCarta(CartaUno carta)
    {
        _mano.Remove(carta);
    }

    // Devuelve la lista de cartas en mano
    public IEnumerable<CartaUno> ObtenerMano()
    {
        return _mano.OfType<CartaUno>().ToList();
    }
}
