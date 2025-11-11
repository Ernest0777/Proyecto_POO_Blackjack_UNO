using System;

namespace BlackJack_1.Interfaces;

public interface IMazo
{
void Barajar();

    int CartasRestantes();

    IEnumerable<ICarta> MostrarCartas();
}
