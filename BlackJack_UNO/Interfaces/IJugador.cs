using System.Collections.Generic;

namespace BlackJack_1.Interfaces
{
public interface IJugador
{
        void RecibirCarta(ICarta carta);
        void MostrarMano();
        void TomarDecision(IJuego juegoContexto);
        int ObtenerPuntos();
        string GetNombre();
        IReadOnlyList<ICarta> GetMano();

        IEstrategiaJugador Estrategia { get; set; }

        void ReportarAccion(string mensaje);

        int GetId();
    }
}
