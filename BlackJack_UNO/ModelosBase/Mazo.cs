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

    // Agregar cartas al mazo
    protected void AgregarCarta(TipoCarta cartaNueva)
        => CartasOriginales.Add(cartaNueva);

    // Baraja las cartas y las guarda en la pila
    public virtual void Barajar()
    {
        var cartasMezcladas = Randomizador.BarajarLista(CartasOriginales);
        PilaDeCartas = new Stack<TipoCarta>(cartasMezcladas);
    }

    // Saca la carta que está hasta arriba
    public virtual TipoCarta SacarCarta()
    {
        if (PilaDeCartas.Count == 0)
            throw new InvalidOperationException("El mazo está vacío, no se pueden sacar más cartas.");

        return PilaDeCartas.Pop();
    }

    // Devuelve la cantidad de cartas restantes en el mazo
    public int CartasRestantes() => PilaDeCartas.Count;

    // Reinsertar una carta al fondo del mazo
    public void ReinsertarCarta(TipoCarta cartaAReinsertar)
    {
        var listaTemporal = PilaDeCartas.Reverse().ToList();
        listaTemporal.Add(cartaAReinsertar);
        PilaDeCartas = new Stack<TipoCarta>(listaTemporal);
    }

    // Devuelve una lista con las cartas actuales del mazo
    public IEnumerable<TipoCarta> MostrarCartas() => PilaDeCartas.ToList();
}
