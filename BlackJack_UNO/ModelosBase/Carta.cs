using System;
using BlackJack_1.Interfaces;
using BlackJack_1.ModelosBase;
namespace BlackJack_1.ModelosBase;

    public class Carta: ICarta
    {
 public string Color { get;protected init; }
        public string Valor { get; protected init; }
        public string Tipo { get; protected init; }

        protected Carta(string color, string valor, string tipo)
        {
            Color = color;
            Valor = valor;
            Tipo = tipo;
        }

    public virtual int GetValorNumerico() => 0;
    

         public virtual void MostrarCarta()
        => Console.WriteLine(ToString());

        public override string ToString()
        => $"{Valor} de {Color} ({Tipo})";
}