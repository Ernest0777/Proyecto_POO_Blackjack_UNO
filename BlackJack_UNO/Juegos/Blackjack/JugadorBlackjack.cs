using System.Linq;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Juegos.Blackjack;

public class JugadorBlackjack : JugadorBase, IJugadorBlackjack
{
    public IEstrategiaJugadorBlackjack? Estrategia { get; set; }

    public JugadorBlackjack(int id, string nombre, IEstrategiaJugadorBlackjack? estrategia)
        : base(id, nombre)
    {
        Estrategia = estrategia;
    }

    // Toma su decisión según su estrategia
    public override void TomarDecision(IJuego juegoContexto)
    {
        if (Estrategia != null)
        {
            Estrategia.EjecutarDecision(this, juegoContexto);
        }
        else
        {
            NotificarAccion("No tiene estrategia definida y pasa su turno.");
        }
    }

    // Calcula los puntos de la mano (considerando el valor flexible del As)
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

    // Sobrescribimos RecibirCarta para notificar cuando llega una carta y detectar bust
    public override void RecibirCarta(ICarta carta)
    {
        base.RecibirCarta(carta);

        // Representación amigable de la carta: si es CartaBlackjack usamos su ToString(), si no usamos la interfaz
        string descripcionCarta = carta is CartaBlackjack cb ? cb.ToString() : carta.ToString();

        int puntos = ObtenerPuntos();
        NotificarAccion($"Recibió {descripcionCarta} → Total actual: {puntos} puntos");

        if (puntos > 21)
            NotificarAccion($"❌ {Nombre} se pasó con {puntos} puntos.");
    }

    // Método auxiliar (opcional) para pedir carta desde estrategia
    public void PedirCarta(MazoBlackjack mazo)
    {
        var carta = mazo.SacarCarta();
        RecibirCarta(carta);
        // Notificaciones ya realizadas en RecibirCarta
    }

    // Método auxiliar para plantarse (opcional)
    public void Plantarse()
    {
        NotificarAccion("Decide plantarse (stand).");
    }
}
