using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Juegos.Blackjack
{
    public class Blackjack : IJuego
    {
        private readonly List<IJugador> _jugadores;
        public IReadOnlyList<IJugador> Jugadores => _jugadores.AsReadOnly();

        public MazoBlackjack Mazo { get; }
        public Dealer Dealer { get; }
        public bool JuegoFinalizado { get; private set; }

        public IEstadoJuego EstadoActual { get; private set; }

        public event Action<string>? OnAccionRegistrada;

        public Blackjack(IEnumerable<IJugador> jugadores)
        {
            _jugadores = jugadores?.ToList() ?? new List<IJugador>();
            Mazo = new MazoBlackjack();
            Dealer = new Dealer(0, "Dealer");

            EstadoActual = new EstadoJuegoBlackjack("Inicializado", _jugadores, Mazo);
        }

        public void IniciarJuego()
        {
            RegistrarAccion("Juego de Blackjack iniciado.");
            RepartirCartas();
            MostrarEstado();
            JugarTurno();
            DeterminarGanador();
            FinalizarJuego();
        }

        public void RepartirCartas()
        {
            foreach (var jugador in _jugadores)
            {
                jugador.RecibirCarta(Mazo.RobarCarta());
                jugador.RecibirCarta(Mazo.RobarCarta());
            }

            Dealer.RecibirCarta(Mazo.RobarCarta());
            Dealer.RecibirCarta(Mazo.RobarCarta());

            RegistrarAccion("Se repartieron las cartas iniciales.");
        }

        public void JugarTurno()
        {
            foreach (var jugador in _jugadores)
            {
                jugador.TomarDecision(this);
                RegistrarAccion($"{jugador.Nombre} termina su turno con {jugador.ObtenerPuntos()} puntos.");
            }

            Dealer.TomarDecision(this);
            RegistrarAccion($"Dealer termina su turno con {Dealer.ObtenerPuntos()} puntos.");
        }

        public void AvanzarTurno() => RegistrarAccion("Avanzando turno.");

        public void DeterminarGanador()
        {
            RegistrarAccion("Determinando ganador(es).");

            int puntosDealer = Dealer.ObtenerPuntos();

            foreach (var jugador in _jugadores)
            {
                int puntosJugador = jugador.ObtenerPuntos();
                string resultado = ReglasBlackjack.DeterminarResultado(puntosJugador, puntosDealer);

                RegistrarAccion($"{jugador.Nombre}: {resultado} ({puntosJugador} vs {puntosDealer})");
                Console.WriteLine($"{jugador.Nombre}: {resultado} ({puntosJugador} vs {puntosDealer})");
            }
        }

        public void FinalizarJuego()
        {
            JuegoFinalizado = true;
            RegistrarAccion("El juego ha finalizado.");
        }

        public string ObtenerEstado() => EstadoActual.ObtenerResumen();

        public void RegistrarAccion(string descripcion)
        {
            OnAccionRegistrada?.Invoke(descripcion);
            EstadoActual = new EstadoJuegoBlackjack(descripcion, _jugadores, Mazo);
        }

        private void MostrarEstado()
        {
            Console.WriteLine("\n--- Estado del Juego ---");
            foreach (var jugador in _jugadores)
                Console.WriteLine($"{jugador.Nombre}: {jugador.ObtenerPuntos()} puntos");
            Console.WriteLine($"Dealer: {Dealer.ObtenerPuntos()} puntos\n");
        }

        private class EstadoJuegoBlackjack : IEstadoJuego
        {
            public string NombreJuego { get; }
            public IReadOnlyList<IJugador> Jugadores { get; }
            public IReadOnlyList<ICarta> Mazo { get; }
            public IReadOnlyList<ICarta> Descarte { get; } = Array.Empty<ICarta>();
            public int TurnoActual { get; }
            private readonly string _descripcion;

            public EstadoJuegoBlackjack(string descripcion, IReadOnlyList<IJugador> jugadores, MazoBlackjack mazo)
            {
                _descripcion = descripcion;
                NombreJuego = "Blackjack";
                Jugadores = jugadores;
                Mazo = mazo.ObtenerCartasRestantes();
                TurnoActual = 0;
            }

            public string ObtenerResumen()
            {
                return $"[{NombreJuego}] {_descripcion} | Jugadores: {Jugadores.Count}, Cartas restantes: {Mazo.Count}";
            }

            public override string ToString() => ObtenerResumen();
        }
    }
}







