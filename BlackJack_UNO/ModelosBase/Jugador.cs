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

    //constructor
    protected JugadorBase(int id, string nombre, IEstrategiaJugador estrategia)
    {
        IdJugador = id;
        Nombre = nombre;
        Estrategia = estrategia;
    }
    
//metodos 
    public virtual void RecibirCarta(ICarta carta)
    {
        if (carta == null) throw new ArgumentNullException(nameof(carta));
        _mano.Add(carta);
         NotificarAccion($"recibió {carta}");
    }

    public virtual void MostrarMano()
    {
        var cartas = string.Join(", ", _mano);
        NotificarAccion($"tiene en mano: {cartas}");
    }
    protected virtual void ReiniciarMano()
    {
    _mano.Clear();
    puntos = 0;
    }

    public virtual void TomarDecision(IJuego juegoContexto)
    {
        Estrategia?.EjecutarDecision(this, juegoContexto);
    }

    public abstract int ObtenerPuntos();

    public string GetNombre() => Nombre;

    public int GetId() => IdJugador;

    public void NotificarAccion(string mensaje)
    {
        if (OnAccionReportada != null)
        {
            string salida = $"[{Nombre}] {mensaje}";
            OnAccionReportada(salida);
        }
    }

}
