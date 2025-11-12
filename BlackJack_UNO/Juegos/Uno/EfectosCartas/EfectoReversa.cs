using System;

namespace BlackJack_1.Juegos.Uno.EfectosCartas;

public class EfectoReversa
namespace BlackJack_1.Juegos.Uno.EfectosCartas
{
    public class EfectoReversa : IEfectoCarta
    {
        public void AplicarEfecto(dynamic juego, dynamic jugadorActual)
        {
            try
            {
                juego.CambiarDireccion();
                juego.RegistrarAccion($"{jugadorActual.Nombre} invirtió la dirección del juego");
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                throw new InvalidOperationException("EfectoReversa: la estructura del juego/jugador no coincide con lo esperado.", ex);
            }
        }
    }
}
