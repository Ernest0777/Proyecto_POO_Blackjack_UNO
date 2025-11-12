using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_1.Interfaces;
using BlackJack_1.ModelosBase;
using BlackJack_1.Utilidades;

namespace BlackJack_1.Juegos.Blackjack;

public class Blackjack : JuegoBase, IJuego
{
    private readonly List<IJugadorBlackjack> jugadores; 

    public Dealer Dealer { get; }
    public MazoBlackjack MazoBlackjack { get; }

    public bool JuegoFinalizado { get; private set; }

    
    public Blackjack(IEnumerable<IJugadorBlackjack> jugadoresParticipantes)
        : base("Blackjack")
    {
        jugadores = jugadoresParticipantes?.ToList() ?? new List<IJugadorBlackjack>();
        MazoBlackjack = new MazoBlackjack();
        Dealer = new Dealer(0, Constantes.NombreDealer);
    }

    // Inicia el juego 
    public override void IniciarJuego()
    {
        if (jugadores.Count == 0)
        {
            RegistrarAccion("No hay jugadores para iniciar el juego.");
            return;
        }

        RegistrarAccion("Juego de Blackjack iniciado.");
        RepartirCartas();
        MostrarEstado();
        JugarTurno();
        DeterminarGanador();
        FinalizarJuego();
    }

    // Reparte las cartas iniciales
    public override void RepartirCartas()
    {
        foreach (var jugador in jugadores)
        {
            for (int i = 0; i < Constantes.CartasInicialesBlackjack; i++)
                jugador.RecibirCarta(MazoBlackjack.SacarCarta());
        }

        for (int i = 0; i < Constantes.CartasInicialesBlackjack; i++)
            Dealer.RecibirCarta(MazoBlackjack.SacarCarta());

        RegistrarAccion("Se repartieron las cartas iniciales.");
    }

    // Cada jugador juega su turno, luego el dealer actúa
    public override void JugarTurno()
    {
        foreach (var jugador in jugadores)
        {
            jugador.TomarDecision(this);
            RegistrarAccion($"{jugador.Nombre} termina su turno con {jugador.ObtenerPuntos()} puntos.");
        }

        Dealer.TomarDecision(this);
        RegistrarAccion($"Dealer termina su turno con {Dealer.ObtenerPuntos()} puntos.");
    }

    public override void AvanzarTurno()
        => RegistrarAccion("Avanzando turno.");

    // Determina el resultado final del juego
    public override void DeterminarGanador()
    {
        RegistrarAccion("Determinando ganador(es).");

        int puntosDealer = Dealer.ObtenerPuntos();

        foreach (var jugador in jugadores)
        {
            int puntosJugador = jugador.ObtenerPuntos();
            string resultado = ReglasBlackjack.DeterminarResultado(puntosJugador, puntosDealer);
            RegistrarAccion($"{jugador.Nombre}: {resultado} ({puntosJugador} vs {puntosDealer})");
        }
    }

    public override void FinalizarJuego()
    {
        JuegoFinalizado = true;
        RegistrarAccion("El juego ha finalizado.");
    }

    // Muestra el estado actual en consola 
    private void MostrarEstado()
    {
        Console.WriteLine("\n Estado del Juego");
        foreach (var jugador in jugadores)
            Console.WriteLine($"{jugador.Nombre}: {jugador.ObtenerPuntos()} puntos");
        Console.WriteLine($"Dealer: {Dealer.ObtenerPuntos()} puntos\n");
    }

    public override void RegistrarAccion(string descripcion)
    {
        base.RegistrarAccion($"[Blackjack] {descripcion}");
    }
}
