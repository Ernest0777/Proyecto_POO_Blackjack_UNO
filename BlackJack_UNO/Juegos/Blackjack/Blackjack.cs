namespace BlackJack_1.Juegos.Blackjack;

using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_1.Interfaces;
using BlackJack_1.ModelosBase;

public class Blackjack : JuegoBase, IJuego
{
    private readonly List<IJugador> jugadores;
    public Dealer Dealer { get; }
    public MazoBlackjack Mazo { get; }

    public bool JuegoFinalizado { get; private set; }

    public Blackjack(IEnumerable<IJugador> jugadoresParticipantes)
        : base("Blackjack")
    {
        jugadores = jugadoresParticipantes?.ToList() ?? new List<IJugador>();
        Mazo = new MazoBlackjack();
        Dealer = new Dealer(0, "Dealer");
    }

    // Inicia el juego y ejecuta el flujo principal
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

    // Reparte dos cartas a cada jugador y al dealer
    public override void RepartirCartas()
    {
        foreach (var jugador in jugadores)
        {
            jugador.RecibirCarta(Mazo.SacarCarta());
            jugador.RecibirCarta(Mazo.SacarCarta());
        }

        Dealer.RecibirCarta(Mazo.SacarCarta());
        Dealer.RecibirCarta(Mazo.SacarCarta());

        RegistrarAccion("Se repartieron las cartas iniciales.");
    }

    // Cada jugador juega su turno luego el dealer actua
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
        => RegistrarAccion("Avanzando turno...");

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
        Console.WriteLine("\n--- Estado del Juego ---");
        foreach (var jugador in jugadores)
            Console.WriteLine($"{jugador.Nombre}: {jugador.ObtenerPuntos()} puntos");
        Console.WriteLine($"Dealer: {Dealer.ObtenerPuntos()} puntos\n");
    }

   
    public override void RegistrarAccion(string descripcion)
    {
        if (OnAccionRegistrada != null)
            OnAccionRegistrada($"[Blackjack] {descripcion}");
    }
}
