using System;
namespace BlackJack_1.Juegos.Uno.EfectosCartas
{

    public class EfectoMasDos : IEfectoCarta
    {
        public void AplicarEfecto(dynamic juego, dynamic jugadorActual)
        {
            try
            {
                var siguiente = juego.ObtenerSiguienteJugador(jugadorActual);
                var cartas = juego.RobarCartas(2);
                siguiente.RecibirCartas(cartas);
                juego.SaltarTurno();
                juego.RegistrarAccion($"{jugadorActual.Nombre} aplicó +2 a {siguiente.Nombre}");
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                
                throw new InvalidOperationException("EfectoMasDos: la estructura del juego/jugador no coincide con lo esperado.", ex);
            }
        }
    }
}
