using System;
namespace BlackJack_1.Juegos.Uno.EfectosCartas
{

    public class EfectoCambioColor : IEfectoCarta
    {
        public void AplicarEfecto(dynamic juego, dynamic jugadorActual)
        {
            try
            {
                var nuevoColor = jugadorActual.ElegirColor();
                juego.CambiarColor(nuevoColor);
                juego.RegistrarAccion($"{jugadorActual.Nombre} cambió el color a {nuevoColor}");
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                throw new InvalidOperationException("EfectoCambioColor: la estructura del juego/jugador no coincide con lo esperado.", ex);
            }
        }
    }
}

