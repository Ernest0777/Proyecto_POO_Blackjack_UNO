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

    // referencia  al descarte
    public Stack<TipoCarta>? PilaDeDescarte { get; set; }

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
        //  Si el mazo esta vacio intenta regenerarlo desde el descarte
        if (PilaDeCartas.Count == 0)
        {
            if (PilaDeDescarte == null || PilaDeDescarte.Count <= 1)
                throw new InvalidOperationException("El mazo y el descarte están vacíos. No se pueden sacar mas cartas.");

            // Conservamos la carta superior (la última jugada)
            var cartaSuperior = PilaDeDescarte.Pop();

            // Rebarajamos el resto del descarte
            var cartasRebarajadas = Randomizador.BarajarLista(PilaDeDescarte.ToList());
            PilaDeCartas = new Stack<TipoCarta>(cartasRebarajadas);

            // Reiniciamos el descarte con solo la carta superior
            PilaDeDescarte.Clear();
            PilaDeDescarte.Push(cartaSuperior);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" El mazo estaba vacio y se rebarajo usando el descarte.");
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
