namespace BlackJack_1.Juegos.Blackjack;

using System.Collections.Generic;
using BlackJack_1.ModelosBase;
using BlackJack_1.Interfaces;

public class MazoBlackjack : Mazo<CartaBlackjack>, IMazo
    {
       public MazoBlackjack()
    {
        GenerarMazo();
        Barajar(); 
    }

          private void GenerarMazo()
    {
        string[] figuras = { "Corazones", "Picas", "Treboles", "Diamantes" };
        string[] valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        foreach (var figura in figuras)
            foreach (var valor in valores)
                AgregarCarta(new CartaBlackjack(figura, valor, "Blackjack"));
    }
}
