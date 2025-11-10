using System.Collections.Generic;
using System;
namespace BlackJack_1.Interfaces;

public interface IJugador
{

    int IdJugador { get; }
    string Nombre { get; }
    
        void RecibirCarta(ICarta carta);
        void TomarDecision(IJuego juegoContexto);
        int ObtenerPuntos();
        
        IReadOnlyList<ICarta> Mano{ get; }

        IEstrategiaJugador Estrategia { get; set; }

        void NotificarAccion(string mensaje);

    event Action<string>? OnAccionReportada;
}

