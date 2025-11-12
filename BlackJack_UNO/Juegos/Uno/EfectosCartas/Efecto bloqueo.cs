using System;
namespace BlackJack_1.Juegos.Uno.EfectosCartas
{

    public class EfectoBloqueo : IEfectoCarta
    {
        public void AplicarEfecto(dynamic juego, dynamic jugadorActual)
        {
            try
            {
                var bloqueado = juego.ObtenerSiguienteJugador(jugadorActual);
                juego.SaltarTurno();
                juego.RegistrarAccion($"{jugadorActual.Nombre} bloqueó a {bloqueado.Nombre}");
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                throw new InvalidOperationException("EfectoBloqueo: la estructura del juego/jugador no coincide con lo esperado.", ex);
            }
        }
    }
}

