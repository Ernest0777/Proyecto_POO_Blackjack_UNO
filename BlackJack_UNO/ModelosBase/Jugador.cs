namespace BlackJack_1.ModelosBase;
using System;
using System.Collections.Generic;
using BlackJack_1.Interfaces;

public abstract class JugadorBase : IJugador
{
    public string Nombre { get; protected init; }
    public int IdJugador { get; protected init; }

        // Lista interna con las cartas que posee el jugador
    protected readonly List<ICarta> _mano = new();
    public IReadOnlyList<ICarta> Mano => _mano.AsReadOnly();
    
    protected int puntos;

    public event Action<string>? OnAccionReportada;

     // Constructor 
    protected JugadorBase(int id, string nombre)
    {
        IdJugador = id;
        Nombre = nombre;
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

    // Metodo abstracto para decidir una accion durante su turno 
    public abstract void TomarDecision(IJuego juegoContexto);

    // Metodo abstracto que devuelve los puntos del jugador
    public abstract int ObtenerPuntos();

    // Envía un mensaje de accion al evento OnAccionReportada
    public void NotificarAccion(string mensaje)
    {
        if (OnAccionReportada != null)
        {
            string salida = $"[{Nombre}] {mensaje}";
            OnAccionReportada(salida);
        }
    }
}
