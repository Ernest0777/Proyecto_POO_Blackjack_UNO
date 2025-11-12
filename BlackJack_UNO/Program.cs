using System;
using System.Collections.Generic;
using BlackJack_1.Interfaces;
using BlackJack_1.Juegos.Blackjack;
using BlackJack_1.Juegos.Uno;
using BlackJack_1.Estrategias;
using BlackJack_1.Utilidades;

namespace BlackJack_1
{
    public static class Program
    {
        public static void Main()
        {
            ConsolaLogger.Mostrar("=== Simulación de Juegos ===");
            ConsolaLogger.Mostrar("1. Blackjack");
            ConsolaLogger.Mostrar("2. UNO");
            ConsolaLogger.Mostrar("Selecciona el juego a ejecutar: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    EjecutarBlackjack();
                    break;
                case "2":
                    EjecutarUno();
                    break;
                default:
                    ConsolaLogger.Advertencia("Opción no válida. Saliendo...");
                    break;
            }
        }

        //        BLACKJACK
        private static void EjecutarBlackjack()
        {
            ConsolaLogger.Mostrar("\n INICIANDO SIMULACIÓN DE BLACKJACK ");

            // Lista tipada con la interfaz específica para Blackjack
            var jugadores = new List<IJugadorBlackjack>
            {
                new JugadorBlackjack(1, "Jugador_Cauteloso", new EstrategiaCautelosa()),
                new JugadorBlackjack(2, "Jugador_Temerario", new EstrategiaTemeraria())
            };

            var blackjack = new Blackjack(jugadores);

            // Vincula el logger a las acciones del juego
            blackjack.OnAccionRegistrada += ConsolaLogger.Mostrar;

            // Ejecuta el flujo principal del juego
            blackjack.IniciarJuego();

            ConsolaLogger.Mostrar("\n FIN DE PARTIDA BLACKJACK \n");
        }

        
        //           UNO
        
        private static void EjecutarUno()
        {
            ConsolaLogger.Mostrar("\n INICIANDO SIMULACIÓN DE UNO ");

            var uno = new Uno();

            // Configurar estrategias iniciales de jugadores
            var estrategias = new List<string> { "Aleatoria", "Calculadora" };
            uno.ConfigurarJugadores(estrategias);

            // Vincular logger
            uno.OnAccionRegistrada += ConsolaLogger.Mostrar;

            // Iniciar simulación
            uno.IniciarJuego();

            ConsolaLogger.Mostrar("\n FIN DE PARTIDA UNO n");
        }
    }
}
