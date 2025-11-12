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
            ConsolaLogger.Mostrar("Simulacion de Juegos");
            ConsolaLogger.Mostrar("1. Blackjack");
            ConsolaLogger.Mostrar("2. UNO");
            ConsolaLogger.Mostrar("Selecciona el juego a ejecutar: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    EjecutarBlackjackInteractivo();
                    break;
                case "2":
                    EjecutarUnoInteractivo();
                    break;
                default:
                    ConsolaLogger.Advertencia("Opción no válida. Saliendo.");
                    break;
            }
        }

       
        //BLACKJACK 
      
        private static void EjecutarBlackjackInteractivo()
        {
            ConsolaLogger.Mostrar("\n INICIANDO SIMULACIÓN DE BLACKJACK \n");

            int cantidadJugadores = SolicitarNumero("¿Cuántos jugadores participarán? (1–6): ", 1, 6);
            var jugadores = new List<IJugadorBlackjack>();

            for (int i = 1; i <= cantidadJugadores; i++)
            {
                ConsolaLogger.Mostrar($"\nSelecciona la estrategia para el Jugador {i}:");
                ConsolaLogger.Mostrar("1. Cauteloso (se planta en 16 o mas)");
                ConsolaLogger.Mostrar("2. Temerario (pide hasta pasar los 17)");

                int opcion = SolicitarNumero("Elige (1 o 2): ", 1, 2);
                IEstrategiaJugadorBlackjack estrategia =
                    opcion == 1 ? new EstrategiaCautelosa() : new EstrategiaTemeraria();

                jugadores.Add(new JugadorBlackjack(i, $"Jugador_{i}", estrategia));
            }

            var blackjack = new Blackjack(jugadores);
            blackjack.OnAccionRegistrada += ConsolaLogger.Mostrar;
            blackjack.IniciarJuego();

            ConsolaLogger.Mostrar("\n FIN DE PARTIDA BLACKJACK \n");
        }

        //    UNO 
        private static void EjecutarUnoInteractivo()
        {
            ConsolaLogger.Mostrar("\n INICIANDO SIMULACIÓN DE UNO \n");

            var uno = new Uno();

            int cantidadJugadores = SolicitarNumero("¿Cuantos jugadores participaran? (2–10): ", 2, 10);
            var estrategias = new List<string>();

            for (int i = 1; i <= cantidadJugadores; i++)
            {
                ConsolaLogger.Mostrar($"\nSelecciona la estrategia para el Jugador {i}:");
                ConsolaLogger.Mostrar("1. Aleatoria");
                ConsolaLogger.Mostrar("2. Calculadora");

                int opcion = SolicitarNumero("Elige (1 o 2): ", 1, 2);
                string tipo = opcion == 1 ? "Aleatoria" : "Calculadora";
                estrategias.Add(tipo);
            }

            uno.ConfigurarJugadores(estrategias);
            uno.OnAccionRegistrada += ConsolaLogger.Mostrar;
            uno.IniciarJuego();

            ConsolaLogger.Mostrar("\n FIN DE PARTIDA UNO \n");
        }

        
        //METODOS AUXILIARES
         
        private static int SolicitarNumero(string mensaje, int minimo, int maximo)
        {
            int valor;
            bool valido;
            do
            {
                ConsolaLogger.Mostrar(mensaje);
                string? entrada = Console.ReadLine();
                valido = int.TryParse(entrada, out valor) && valor >= minimo && valor <= maximo;

                if (!valido)
                    ConsolaLogger.Advertencia($"Por favor ingresa un numero entre {minimo} y {maximo}.");
            }
            while (!valido);

            return valor;
        }
    }
}
