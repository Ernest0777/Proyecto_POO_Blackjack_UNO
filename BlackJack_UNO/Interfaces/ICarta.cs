using System;

namespace BlackJack_1.Interfaces;

public interface ICarta
{   
    string Color { get; }
    string Valor { get; }
    string Tipo { get; }

    string NombreCorto{ get; }
    int ObtenerValorNumerico();
}
