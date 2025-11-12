namespace BlackJack_1.Juegos.Blackjack;

    public static class ReglasBlackjack
{
        private const int ValorMaximo = 21;
    // Verifica si el jugador tiene 21 con dos cartas

        public static bool EsBlackjack(int puntos, int cantidadCartas)
        {
            return puntos == ValorMaximo && cantidadCartas == 2;
        }

        public static bool SePaso(int puntos)
        {
            return puntos > ValorMaximo;
        }

        public static string DeterminarResultado(int puntosJugador, int puntosDealer)
        {
            if (puntosJugador > 21)
                return "Pierde (se pasó de 21)";
            if (puntosDealer > 21)
                return "Gana (dealer se pasó)";
            if (puntosJugador > puntosDealer)
                return "Gana";
            if (puntosJugador == puntosDealer)
                return "Empate";
            return "Pierde";
        }
    }



