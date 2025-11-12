namespace BlackJack_1.Juegos.Uno;

using System.Collections.Generic;
using System.Linq;
using BlackJack_1.ModelosBase;
using BlackJack_1.CreadorDeJugadores;
using BlackJack_1.Utilidades;

public class Uno : JuegoBase
{
   private readonly MazoUno mazo = new();
    private readonly MazoUno mazo = new();
    private readonly List<JugadorUno> jugadores = new();
    private readonly Stack<CartaUno> pilaDescarte = new();
    private int direccionTurnos = 1; // 1 = horario, -1 = antihorario
    private int indiceActual = 0;
    private bool JuegoFinalizado = false;
    private bool juegoFinalizado = false;

    public Uno() : base("UNO") { }

    // Crea los jugadores y les reparte sus cartas iniciales.
   public void ConfigurarJugadores(IEnumerable<string> tiposEstrategias)
    public void ConfigurarJugadores(IEnumerable<string> tiposEstrategias)
    {
        var creador = new CreadorDeJugadores();

        foreach (var tipo in tiposEstrategias)
        {
            var jugador = creador.CrearJugadorUno($"Jugador_{tipo}", tipo);
            jugadores.Add(jugador);
        }

        // Repartir 4 cartas a cada jugador
        // Repartir las cartas iniciales
        foreach (var jugador in jugadores)
            for (int carta = 0; carta < Constantes.CartasInicialesUno; carta++)
            for (int i = 0; i < Constantes.CartasInicialesUno; i++)
                jugador.AgregarCarta(mazo.SacarCarta());

        // Colocar la primera carta en la pila de descarte
       pilaDescarte.Push(mazo.SacarCarta());
        pilaDescarte.Push(mazo.SacarCarta());
        ConsolaLogger.Mostrar($"Carta inicial: {pilaDescarte.Peek()}");
    }

    
    ///Inicia la simulacion de UNO.
    
    // Inicia la simulación del juego UNO
    public override void IniciarJuego()
    {
        ConsolaLogger.Mostrar(" Comienza el juego de UNO, mucha suerte jugadores, que gane el mejor");
        ConsolaLogger.Mostrar("Comienza el juego de UNO, mucha suerte jugadores, que gane el mejor");

        while (!JuegoFinalizado)
        while (!juegoFinalizado)
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
                // Aplica los efectos de la carta jugada
                ReglasUno.AplicarEfecto(
                    cartaSeleccionada,
                    ref direccionTurnos,
                    new Queue<JugadorUno>(jugadores),
                    mazo,
                    jugadorActual
                );

                // Verificar si el jugador gano
                // Verificar si el jugador ganó
                if (ReglasUno.HaGanado(jugadorActual))
                {
                    ConsolaLogger.Mostrar($"\n {jugadorActual.Nombre} ha ganado la partida de UNO");
                    JuegoFinalizado = true;
                    ConsolaLogger.Mostrar($"\n{jugadorActual.Nombre} ha ganado la partida de UNO");
                    juegoFinalizado = true;
                    break;
                }

                // Grita UNO si tiene una carta
                // Grita UNO si tiene solo una carta
                if (jugadorActual.ObtenerMano().Count() == 1)
                    ConsolaLogger.Mostrar($" {jugadorActual.Nombre} grita: UNOOOOOOOOOOOOOOO");
                    ConsolaLogger.Mostrar($"{jugadorActual.Nombre} grita: ¡UNOOOOOO!");
            }
            else
            {
                // Toma una carta por no poder colocar una carta
                // Si no puede jugar, roba una carta
                var cartaTomada = mazo.SacarCarta();
                jugadorActual.AgregarCarta(cartaTomada);
                ConsolaLogger.Mostrar($"{jugadorActual.Nombre} no puede jugar y toma una carta ({cartaTomada}).");
            }

            // Pasar al siguiente jugador dependiendo sentido del juego
            // Cambiar al siguiente jugador (considerando la dirección del juego)
            indiceActual = (indiceActual + direccionTurnos + jugadores.Count) % jugadores.Count;

            // Registrar el estado de este turno
            RegistrarAccion($"Turno finalizado. Proximo jugador: {jugadores[indiceActual].Nombre}");
            // Registrar el turno
            RegistrarAccion($"Turno finalizado. Próximo jugador: {jugadores[indiceActual].Nombre}");
        }

        ConsolaLogger.Mostrar("\n Fin del juego ");
        ConsolaLogger.Mostrar("\nFin del juego.");
        FinalizarJuego();
    }
// Devuelve la carta actual en la cima de la pila de descarte
public CartaUno ObtenerCartaSuperior()
{
    if (pilaDescarte.Count == 0)
        throw new InvalidOperationException("No hay cartas en la pila de descarte.");

    return pilaDescarte.Peek();
}

    public override void FinalizarJuego()
    {
        JuegoFinalizado = true;
        juegoFinalizado = true;
        RegistrarAccion("El juego de UNO ha finalizado.");
    }

    // Registrar acciones en log
    // 🔹 Método sobrescrito correctamente (sin error CS0070)
    public override void RegistrarAccion(string descripcion)
    {
        if (OnAccionRegistrada != null)
            OnAccionRegistrada($"[UNO] {descripcion}");
        base.RegistrarAccion($"[UNO] {descripcion}");
    }
}
