using BlackJack_1.Interfaces;
using BlackJack_1.ModelosBase;
namespace BlackJack_1.Juegos.Blackjack
{
    public class Dealer : JugadorBlackjack
    {
        public Dealer(int id, string nombre) : base(id, nombre, estrategia: null) { }

        public void BarajarMazo(MazoBlackjack mazo)
        {
            mazo.Barajar();
        NotificarAccion("barajo el mazo.");
        }

        public override void TomarDecision(IJuego juegoContexto)
        {
            // El dealer pide carta hasta tener 17 o más puntos
            if (juegoContexto is not Blackjack blackjack) return;

            while (ObtenerPuntos() < 17)
            {
                var carta = blackjack.Mazo.SacarCarta();
            RecibirCarta(carta);
            NotificarAccion($"recibio {carta} (total: {ObtenerPuntos()} puntos).");
        }

            NotificarAccion($"terminó su turno con {ObtenerPuntos()} puntos.");
        }
    }
}


