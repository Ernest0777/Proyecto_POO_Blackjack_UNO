using System;
using BlackJack_1.Interfaces;

namespace BlackJack_1.Utilidades;

public static class ValidadorReglas
{
    // Verifica si hay jugadores
    public static bool ValidarJugadoresMinimos(IJuego juego, int minimo)
    {
        if (juego.EstadoActual.Jugadores.Count < minimo)
        {
            ConsolaLogger.Error($"No hay suficientes jugadores para iniciar {juego.EstadoActual.NombreJuego}.");
            return false;
        }
        return true;
    }

    // Verifica si el jugador tiene una mano válida
    public static bool ValidarManoJugador(IJugador jugador)
    {
        if (jugador == null)
        {
            ConsolaLogger.Error("Jugador nulo detectado en validación.");
            return false;
        }

        if (jugador.Mano == null || jugador.Mano.Count == 0)
        {
            ConsolaLogger.Advertencia($"El jugador {jugador.Nombre} no tiene cartas en mano.");
            return false;
        }

        return true;
    }

    // Verifica si el juego aún no ha terminado
    public static bool ValidarJuegoActivo(bool juegoFinalizado)
    {
        if (juegoFinalizado)
        {
            ConsolaLogger.Advertencia("El juego ya ha finalizado, no se pueden realizar más acciones.");
            return false;
        }

        return true;
    }

    // Verifica si el descarte es válido
    public static bool ValidarDescarteDisponible(IJuego juego)
    {
        if (juego.EstadoActual.Descarte == null || juego.EstadoActual.Descarte.Count == 0)
        {
            ConsolaLogger.Advertencia($"El juego {juego.EstadoActual.NombreJuego} no tiene cartas en la pila de descarte.");
            return false;
        }

        return true;
    }

    // Valida los puntos del jugador (para el Blackjack)
    public static bool ValidarPuntajeBlackjack(IJugador jugador)
    {
        if (jugador.ObtenerPuntos() < 0)
        {
            ConsolaLogger.Error($"Puntaje inválido detectado para el jugador {jugador.Nombre}.");
            return false;
        }

        return true;
    }
}