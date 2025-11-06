namespace BlackJack_1.ModelosBase;
//INCOMPLETO 
using System;
using System.Collections.Generic;
using BlackJack_1.Interfaces;

public abstract class JuegoBase : IJuego
{
    public string NombreJuego { get; protected init; } = "Juego";
    public IReadOnlyList<IJugador> Jugadores => _jugadores.AsReadOnly();
    public IReadOnlyList<ICarta> Mazo => _mazo.AsReadOnly();
    public IReadOnlyList<ICarta> Descarte => _descarte.AsReadOnly();
    public int TurnoActual { get; protected set; }

    public IEstadoJuego EstadoActual => new EstadoJuegoBasico(this);

    protected readonly List<IJugador> _jugadores = new();
    protected readonly List<ICarta> _mazo = new();
    protected readonly List<ICarta> _descarte = new();

    public event Action<string>? OnAccionRegistrada;

    protected JuegoBase(string nombreJuego)
    {
        NombreJuego = nombreJuego;
    }

    public virtual void IniciarJuego()
        => RegistrarAccion($"Iniciando {NombreJuego} con {_jugadores.Count} jugadores.");

    public virtual void RepartirCartas()
        => RegistrarAccion("Repartiendo cartas...");

    public virtual void JugarTurno()
        => RegistrarAccion($"Turno del jugador #{TurnoActual + 1}");

    public virtual void AvanzarTurno()
    {
        if (_jugadores.Count == 0) return;
        TurnoActual = (TurnoActual + 1) % _jugadores.Count;
    }

    public virtual void DeterminarGanador()
        => RegistrarAccion("Determinando ganador...");

    public virtual void FinalizarJuego()
        => RegistrarAccion($"Finalizando {NombreJuego}");

    public virtual string ObtenerEstado()
        => $"[{NombreJuego}] Jugadores: {_jugadores.Count}, Mazo: {_mazo.Count}, Descarte: {_descarte.Count}, Turno: {TurnoActual + 1}";

    public virtual void RegistrarAccion(string descripcion)
        => OnAccionRegistrada?.Invoke(descripcion);

    private sealed class EstadoJuegoBasico : IEstadoJuego
    {
        private readonly JuegoBase _juego;

        public EstadoJuegoBasico(JuegoBase juego)
        {
            _juego = juego;
        }

        public string NombreJuego => _juego.NombreJuego;
        public IReadOnlyList<IJugador> Jugadores => _juego.Jugadores;
        public IReadOnlyList<ICarta> Mazo => _juego.Mazo;
        public IReadOnlyList<ICarta> Descarte => _juego.Descarte;
        public int TurnoActual => _juego.TurnoActual;

        public string ObtenerResumen() => _juego.ObtenerEstado();
    }
}
