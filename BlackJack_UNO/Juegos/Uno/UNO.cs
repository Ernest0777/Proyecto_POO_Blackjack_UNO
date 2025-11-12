namespace BlackJack_1.Juegos.Uno;
using System.Collections.Generic;
using System.Linq;
using BlackJack_1.ModelosBase;
using BlackJack_1.CreadorDeJugadores;
using BlackJack_1.Utilidades;
public class Uno : JuegoBase
{
   private readonly MazoUno mazo = new();
    private readonly List<JugadorUno> jugadores = new();
    private readonly Stack<CartaUno> pilaDescarte = new();
    private int direccionTurnos = 1; // 1 = horario, -1 = antihorario
    private int indiceActual = 0;
    private bool JuegoFinalizado = false;

    public Uno() : base("UNO") { }

    // Crea los jugadores y les reparte sus cartas iniciales.
   public void ConfigurarJugadores(IEnumerable<string> tiposEstrategias)
    {
        var creador = new CreadorDeJugadores();

        foreach (var tipo in tiposEstrategias)
        {
            var jugador = creador.CrearJugadorUno($"Jugador_{tipo}", tipo);
            jugadores.Add(jugador);
        }

        // Repartir 4 cartas a cada jugador
        foreach (var jugador in jugadores)
            for (int carta = 0; carta < Constantes.CartasInicialesUno; carta++)
                jugador.AgregarCarta(mazo.SacarCarta());

        // Colocar la primera carta en la pila de descarte
       pilaDescarte.Push(mazo.SacarCarta());
        ConsolaLogger.Mostrar($"Carta inicial: {pilaDescarte.Peek()}");
    }

    
    ///Inicia la simulacion de UNO.
    
    public override void IniciarJuego()
    {
        ConsolaLogger.Mostrar(" Comienza el juego de UNO, mucha suerte jugadores, que gane el mejor");

        while (!JuegoFinalizado)
        {
            var jugadorActual = jugadores[indiceActual];
            var cartaSuperior = pilaDescarte.Peek();

            ConsolaLogger.Mostrar($"\nTurno de {jugadorActual.Nombre}:");
            var cartaSeleccionada = jugadorActual.Estrategia.DecidirCarta(jugadorActual, cartaSuperior);

            if (cartaSeleccionada != null && ReglasUno.PuedeJugar(cartaSuperior, cartaSeleccionada))
            {
                jugadorActual.QuitarCarta(cartaSeleccionada);
                pilaDescarte.Push(cartaSeleccionada);
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} juega {cartaSeleccionada}");

                // Aplicar efectos especiales de la carta jugada
                ReglasUno.AplicarEfecto(
                    cartaSeleccionada,
                    ref direccionTurnos,
                    new Queue<JugadorUno>(jugadores),
                    mazo,
                    jugadorActual
                );

                // Verificar si el jugador gano
                if (ReglasUno.HaGanado(jugadorActual))
                {
                    ConsolaLogger.Mostrar($"\n {jugadorActual.Nombre} ha ganado la partida de UNO");
                    JuegoFinalizado = true;
                    break;
                }

                // Grita UNO si tiene una carta
                if (jugadorActual.ObtenerMano().Count() == 1)
                    ConsolaLogger.Mostrar($" {jugadorActual.Nombre} grita: UNOOOOOOOOOOOOOOO");
            }
            else
            {
                // Toma una carta por no poder colocar una carta
                var cartaTomada = mazo.SacarCarta();
                jugadorActual.AgregarCarta(cartaTomada);
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} no puede jugar y toma una carta ({cartaTomada}).");
            }

            // Pasar al siguiente jugador dependiendo sentido del juego
            indiceActual = (indiceActual + direccionTurnos + jugadores.Count) % jugadores.Count;

            // Registrar el estado de este turno
            RegistrarAccion($"Turno finalizado. Proximo jugador: {jugadores[indiceActual].Nombre}");
        }

        ConsolaLogger.Mostrar("\n Fin del juego ");
        FinalizarJuego();
    }

    public override void FinalizarJuego()
    {
        JuegoFinalizado = true;
        RegistrarAccion("El juego de UNO ha finalizado.");
    }

    // Registrar acciones en log
    public override void RegistrarAccion(string descripcion)
    {
        if (OnAccionRegistrada != null)
            OnAccionRegistrada($"[UNO] {descripcion}");
    }
}
