namespace BlackJack_1.ModelosBase;

using System;
using System.Collections.Generic;
using BlackJack_1.Interfaces;
using BlackJack_1.Utilidades;

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
    {
        if (!ValidarJugadores())
            throw new InvalidOperationException("No hay jugadores validos para iniciar el juego.");

        RegistrarAccion($"Iniciando {NombreJuego} con {_jugadores.Count} jugadores...");
        BarajarMazo();
    }

    public virtual void RepartirCartas()
        => RegistrarAccion("Repartiendo cartas");

    public virtual void JugarTurno()
        => RegistrarAccion($"Turno del jugador #{TurnoActual + 1}");

    public virtual void AvanzarTurno()
    {
        if (_jugadores.Count == 0) return;
        TurnoActual = (TurnoActual + 1) % _jugadores.Count;
    }

    public virtual void DeterminarGanador()
        => RegistrarAccion("Determinando ganador");

    public virtual void FinalizarJuego()
        => RegistrarAccion($"Finalizando {NombreJuego}");

    public virtual string ObtenerEstado()
        => $"[{NombreJuego}] Jugadores: {_jugadores.Count}, Mazo: {_mazo.Count}, Descarte: {_descarte.Count}, Turno: {TurnoActual + 1}";

    public virtual void RegistrarAccion(string descripcion)
    {
        if (OnAccionRegistrada != null)
            OnAccionRegistrada(descripcion);
    }

    protected virtual bool ValidarJugadores()
    {
        if (NombreJuego.Equals("UNO", StringComparison.OrdinalIgnoreCase))
            return _jugadores.Count >= Constantes.NumeroJugadoresMinimoUno;

        if (NombreJuego.Equals("Blackjack", StringComparison.OrdinalIgnoreCase))
            return _jugadores.Count >= 1;

        return _jugadores.Count > 0;
    }

    protected virtual void BarajarMazo()
    {
        if (_mazo.Count == 0) return;

        var cartasBarajadas = Randomizador.BarajarLista(_mazo);
        _mazo.Clear();
        _mazo.AddRange(cartasBarajadas);

        RegistrarAccion("El mazo ha sido barajado.");
    }

    protected virtual void ReiniciarJuego()
    {
        _mazo.Clear();
        _descarte.Clear();
        TurnoActual = 0;
        RegistrarAccion("El juego ha sido reiniciado.");
    }

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
