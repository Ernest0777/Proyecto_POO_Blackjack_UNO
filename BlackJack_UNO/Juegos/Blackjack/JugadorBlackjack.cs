using System.Linq;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Juegos.Blackjack;

public class JugadorBlackjack : JugadorBase, IJugadorBlackjack
{
    // Estrategia puede ser nula (por ejemplo, en el caso del Dealer)
    public IEstrategiaJugadorBlackjack? Estrategia { get; set; }

    // constructor recibe id nombre y la estrategia concreta
    public JugadorBlackjack(int id, string nombre, IEstrategiaJugadorBlackjack? estrategia)
        : base(id, nombre)
    {
        Estrategia = estrategia;
    }

    // dependiendo de la estrategia toma una decision
    public override void TomarDecision(IJuego juegoContexto)
    {
        if (Estrategia != null)
            Estrategia.EjecutarDecision(this, juegoContexto);
        else
            NotificarAccion("no tiene estrategia definida y pasa su turno.");
    }

    // Calcula los puntos de la mano considerando los as
    public override int ObtenerPuntos()
    {
        int puntosTotales = _mano.Sum(c => c.ObtenerValorNumerico());
        int cantidadAses = _mano.Count(c => c is CartaBlackjack carta && carta.Valor == "A");

        while (puntosTotales > 21 && cantidadAses > 0)
        {
            puntosTotales -= 10; // El As pasa de valer 11 a valer 1
            cantidadAses--;
        }

        return puntosTotales;
    }
}
