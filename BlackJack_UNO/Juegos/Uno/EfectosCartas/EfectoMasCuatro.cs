using System;

namespace BlackJack_1.Juegos.Uno.EfectosCartas;

public class EfectoMasCuatro
namespace BlackJack_1.Juegos.Uno.EfectosCartas
{
    public class EfectoMasCuatro : IEfectoCarta
    {
        public void AplicarEfecto(dynamic juego, dynamic jugadorActual)
        {
            try
            {
                var siguiente = juego.ObtenerSiguienteJugador(jugadorActual);
                var cartas = juego.RobarCartas(4);
                siguiente.RecibirCartas(cartas);
                juego.SaltarTurno();

                var nuevoColor = jugadorActual.ElegirColor();
                juego.CambiarColor(nuevoColor);

                juego.RegistrarAccion($"{jugadorActual.Nombre} aplicó +4 a {siguiente.Nombre} y cambió el color a {nuevoColor}");
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                throw new InvalidOperationException("EfectoMasCuatro: la estructura del juego/jugador no coincide con lo esperado.", ex);
            }
        }
    }
}

