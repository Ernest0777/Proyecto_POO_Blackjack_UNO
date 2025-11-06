using System;

namespace BlackJack_1.Interfaces;

public interface IJuego
{
    void IniciarJuego();
    void RepartirCartas();
    void JugarTurno();
    void AvanzarTurno();
    void DeterminarGanador();
    void FinalizarJuego();

    IEstadoJuego EstadoActual{ get; }

    string ObtenerEstado();
    void RegistrarAccion(string descripcion);

        //Permite notificar a un Logger u observador sin acoplarlo directamente
        event Action<string>? OnAccionRegistrada;

}
