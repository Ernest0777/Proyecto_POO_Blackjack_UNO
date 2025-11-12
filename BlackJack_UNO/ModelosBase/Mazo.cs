using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_1.Interfaces;
using BlackJack_1.Utilidades;

namespace BlackJack_1.ModelosBase;

public abstract class Mazo<TipoCarta> : IMazo<TipoCarta> where TipoCarta : ICarta
{
    protected List<TipoCarta> CartasOriginales { get; } = new();
    protected Stack<TipoCarta> PilaDeCartas { get; private set; } = new();

    //  pila de descarte (para regenerar mazo)
    public Stack<TipoCarta>? PilaDeDescarte { get; set; }

    // contador de rebarajeos
    private int rebarajeosRealizados = 0;

    // configurador de rebarajeos maximos
    private const int MaxRebarajeosPermitidos = 10;

    protected void AgregarCarta(TipoCarta cartaNueva)
    {
        CartasOriginales.Add(cartaNueva);
    }

    public virtual void Barajar()
    {
        var cartasMezcladas = Randomizador.BarajarLista(CartasOriginales);
        PilaDeCartas = new Stack<TipoCarta>(cartasMezcladas);
    }

    public virtual TipoCarta SacarCarta()
    {
        // Si el mazo se vacía, intentamos regenerarlo desde el descarte
        if (PilaDeCartas.Count == 0)
        {
            if (PilaDeDescarte == null || PilaDeDescarte.Count <= 1)
                throw new InvalidOperationException("El mazo y el descarte están vacios. No se pueden sacar mas cartas.");

            // Verificar si se supero el limite de rebarajeos
            if (rebarajeosRealizados >= MaxRebarajeosPermitidos)
                throw new InvalidOperationException("Empate: se alcanzo el límite maximo de rebarajeos.");

            rebarajeosRealizados++;

            // Conservamos la carta superior (última jugada)
            var cartaSuperior = PilaDeDescarte.Pop();

            // Rebarajamos las demás cartas del descarte
            var cartasRebarajadas = Randomizador.BarajarLista(PilaDeDescarte.ToList());
            PilaDeCartas = new Stack<TipoCarta>(cartasRebarajadas);

            // Reiniciamos el descarte dejando solo la carta superior
            PilaDeDescarte.Clear();
            PilaDeDescarte.Push(cartaSuperior);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" El mazo estaba vacio. Rebaraje #{rebarajeosRealizados} realizado usando el descarte.");
            Console.ResetColor();
        }

        return PilaDeCartas.Pop();
    }

    public int CartasRestantes() => PilaDeCartas.Count;

    public void ReinsertarCarta(TipoCarta cartaAReinsertar)
    {
        var listaTemporal = PilaDeCartas.Reverse().ToList();
        listaTemporal.Add(cartaAReinsertar);
        PilaDeCartas = new Stack<TipoCarta>(listaTemporal);
    }

    public IEnumerable<TipoCarta> MostrarCartas() => PilaDeCartas.ToList();
}
