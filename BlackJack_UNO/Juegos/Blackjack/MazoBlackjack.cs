using System.Collections.Generic;
using System.Linq;
using BlackJack_1.Utilidades;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Juegos.Blackjack
{
    public class MazoBlackjack
    {
        private readonly List<CartaBlackjack> _cartas;
        private int _indice;

        public MazoBlackjack()
        {
            _cartas = GenerarMazo();
            Mezclar();
        }

        private List<CartaBlackjack> GenerarMazo()
        {
            string[] figuras = { "Corazones", "Picas", "Tréboles", "Diamantes" };
            string[] valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            var cartas = new List<CartaBlackjack>();
            foreach (var figura in figuras)
                foreach (var valor in valores)
                    cartas.Add(new CartaBlackjack(figura, valor, "Blackjack"));

            return cartas;
        }

        public void Mezclar()
        {
            var mezcladas = Randomizador.BarajarLista(_cartas);
            _cartas.Clear();
            _cartas.AddRange(mezcladas);
            _indice = 0;
        }

        public CartaBlackjack RobarCarta()
        {
            if (_indice >= _cartas.Count)
                Mezclar();

            return _cartas[_indice++];
        }

        public int CartasRestantes => _cartas.Count - _indice;

        public IReadOnlyList<ICarta> ObtenerCartasRestantes()
        {
            return _cartas.Skip(_indice).Cast<ICarta>().ToList().AsReadOnly();
        }
    }
}



