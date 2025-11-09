namespace BlackJack_1.ModelosBase;
using System;
using System.Collections.Generic;
using BlackJack_1.Interfaces;

public abstract class JugadorBase : IJugador
{
    public string Nombre { get; protected init; }
    public int IdJugador { get; protected init; }
    protected readonly List<ICarta> _mano = new();
    public IReadOnlyList<ICarta> Mano => _mano.AsReadOnly();
    protected int puntos;
    public IEstrategiaJugador Estrategia { get; set; }

    public event Action<string>? OnAccionReportada;

    protected JugadorBase(int id, string nombre, IEstrategiaJugador estrategia)
    {
        IdJugador = id;
        Nombre = nombre;
        Estrategia = estrategia;
    }

    public virtual void RecibirCarta(ICarta carta)
    {
        if (carta == null) throw new ArgumentNullException(nameof(carta));
        _mano.Add(carta);
        ReportarAccion($"recibio {carta}");
    }

    public virtual void MostrarMano()
    {
        var cartas = string.Join(", ", _mano);
        ReportarAccion($"tiene en mano: {cartas}");
    }

    public virtual void TomarDecision(IJuego juegoContexto)
    {
        Estrategia?.EjecutarDecision(this, juegoContexto);
    }

    public abstract int ObtenerPuntos();

    public string GetNombre() => Nombre;

    public int GetId() => IdJugador;

    protected void ReportarAccion(string mensaje)
    {
        DispararAccion($"[{Nombre}] {mensaje}");
    }

    private void DispararAccion(string mensaje)
    {
        if (OnAccionReportada != null)
            OnAccionReportada(mensaje);
    }
}
