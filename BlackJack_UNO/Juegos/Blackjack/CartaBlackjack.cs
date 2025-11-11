using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

using BlackJack_1.ModelosBase;

namespace BlackJack_1.Juegos.Blackjack
{
    public class CartaBlackjack : Carta
    {
        public string Figura { get; }

        public CartaBlackjack(string figura, string valor, string tipo)
            : base(color: figura, valor: valor, tipo: tipo)
        {
            Figura = figura;
        }

        public override int ObtenerValorNumerico()
        {
            return Valor switch
            {
                "J" or "Q" or "K" => 10,
                "A" => 11, // El As se ajusta luego si se pasa de 21
                _ => int.TryParse(Valor, out int n) ? n : 0
            };
        }

        public override string ToString()
        {
            return $"{Valor} de {Figura}";
        }
    }
}


