using System.Linq;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Juegos.Blackjack
{
    public class JugadorBlackjack : JugadorBase
    {
        public JugadorBlackjack(int id, string nombre, IEstrategiaJugador estrategia)
            : base(id, nombre, estrategia) { }

        public override int ObtenerPuntos()
        {
            int puntosTotales = _mano.Sum(c => c.ObtenerValorNumerico());
            int ases = _mano.Count(c => c is CartaBlackjack cb && cb.Valor == "A");

            while (puntosTotales > 21 && ases > 0)
            {
                puntosTotales -= 10; // El As pasa de valer 11 a valer 1
                ases--;
            }

            return puntosTotales;
        }
    }
}


