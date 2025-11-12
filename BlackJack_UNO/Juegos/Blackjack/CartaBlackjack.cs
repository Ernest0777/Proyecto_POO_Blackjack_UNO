using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Juegos.Blackjack;

    public class CartaBlackjack : Carta
    {
    // la propiedad Color de la clase base representa el palo 

        public string Palo => Color;

        // Constructor recibe el palo y el valor, el tipo se determina automáticamente según el valor
    public CartaBlackjack(string palo, string valor)
        : base(color: palo, valor: valor, tipo: DeterminarTipo(valor))
    {
    }

    // Determina el tipo de carta según su valor
    private static string DeterminarTipo(string valor)
    {
        return valor switch
        {
            "J" or "Q" or "K" => "figura", // Cartas de figura
            "A" => "as",                   // As
            _ => "normal"                  // Cartas numéricas (2–10)
        };
    }
        // Devuelve el valor numerico de la carta según las reglas del Blackjack
        public override int ObtenerValorNumerico()
        {
            return Tipo switch
            {
                "figura" => 10,
                "as" => 11,     // El As vale 11 por defecto
                _ => int.TryParse(Valor, out int numero) ? numero : 0
            };
        }

        public override string ToString()
    {
        return $"{Valor} de {Palo} ({Tipo})";
    }
}



