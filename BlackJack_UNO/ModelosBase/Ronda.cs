namespace BlackJack_1.ModelosBase;

using System;
using System.Collections.Generic;
using BlackJack_1.Interfaces;

public abstract class Ronda
{
    protected readonly List<IJugador> jugadores;

    // Referencia al juego que contiene esta ronda
    protected readonly IJuego juego;

    protected int indiceJugadorActual;

    // Bandera para indicar si la ronda sigue activa
    public bool RondaActiva { get; protected set; } = true;

    // Evento para registrar acciones para logs
    public event Action<string>? OnAccionRegistrada;

    // Constructor principal
    protected Ronda(IJuego juegoActual, List<IJugador> jugadoresParticipantes)
    {
        if (juegoActual == null)
            throw new ArgumentNullException(nameof(juegoActual));
        if (jugadoresParticipantes == null || jugadoresParticipantes.Count == 0)
            throw new ArgumentException("La ronda debe tener al menos un jugador.");

        juego = juegoActual;
        jugadores = jugadoresParticipantes;
        indiceJugadorActual = 0;
    }

    // Iniciar la ronda
    public virtual void IniciarRonda()
    {
        RondaActiva = true;
        RegistrarAccion($"Iniciando ronda con {jugadores.Count} jugadores.");
        PrepararRonda();
    }

    // Metodo opcional para inicializar condiciones de la ronda
    protected virtual void PrepararRonda()
    {
    }

    // Ejecuta un turno del jugador actual
    public virtual void EjecutarTurno()
    {
        if (!RondaActiva)
            return;

        var jugadorActual = jugadores[indiceJugadorActual];
        RegistrarAccion($"Turno de {jugadorActual.Nombre}");

        jugadorActual.TomarDecision(juego);
        AvanzarTurno();
    }

    // Avanza al siguiente jugador (se puede sobreescribir en uno)
    protected virtual void AvanzarTurno()
    {
        indiceJugadorActual = (indiceJugadorActual + 1) % jugadores.Count;
    }

    // Finaliza la ronda 
    public virtual void FinalizarRonda(string motivo = "Condicion de final cumplida")
    {
        RondaActiva = false;
        RegistrarAccion($"Ronda finalizada: {motivo}");
    }

    // Metodo abstracto cada tipo de ronda define sus condiciones de fin
    public abstract bool VerificarFinDeRonda();

    // Registro de mensaje
    protected void RegistrarAccion(string mensaje)
    {
        if (OnAccionRegistrada != null)
            OnAccionRegistrada($"[Ronda] {mensaje}");
    }
}
