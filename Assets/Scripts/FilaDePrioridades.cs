using UnityEngine;
using System.Collections;
using System;

public class FilaDePrioridades<T> : MonoBehaviour
{

    No primeiro;

   class No : IComparable<No>
    {
        public int peso;
        public No anterior;
        public T valor;

        public No(T valor, int peso)
        {
            this.valor = valor;
            this.peso = peso;
        }

        public int CompareTo(No obj)
        {
            if (obj == null) return 1;

            if (this.peso == obj.peso) return 1;

            return this.peso - obj.peso;
        }

        public override string ToString()
        {
            return "Valor: " + valor.ToString() + " Peso: " + peso;
        }

    }

    public void Add(T obj, int peso)
    {

        No novo = new No(obj, peso);

        if(primeiro == null)
        {
            primeiro = novo;
            return;
        }

        if (novo.CompareTo(primeiro) > 0)
        {
            novo.anterior = primeiro;
            primeiro = novo;
            return;
        }

        No atual = primeiro;

        while (atual.anterior != null && novo.CompareTo(atual.anterior) < 0)
        {
            atual = atual.anterior;
        }

        novo.anterior = atual.anterior;
        atual.anterior = novo;

    }

    public override string ToString()
    {
        No atual = primeiro;
        String retorno = "";
        int elementos = 0;

        retorno += "(";

        while (atual != null)
        {
            elementos++;
            retorno += "[" + atual.ToString() +"]";
            atual = atual.anterior;
        }

        retorno += ")";

        return "Nos: " + elementos + ", Valores:" + retorno;
    }

}
