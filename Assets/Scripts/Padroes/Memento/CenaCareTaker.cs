using System.Collections.Generic;
using UnityEngine;

public class CenaCareTaker {

    private readonly int LIMITE_DE_DESFAZER = 12;
    private LinkedList<CenaMemento> estadosCena;

    public CenaCareTaker()
    {
        estadosCena = new LinkedList<CenaMemento>();
    }

    public void adicionarMemento(CenaMemento memento)
    {
        estadosCena.AddFirst(memento);

       if (estadosCena.Count > LIMITE_DE_DESFAZER)
            estadosCena.RemoveLast();
    }

    public CenaMemento getUltimoEstadoSalvo()
    {
        if (contemElementos())
        {
            CenaMemento estadoSalvo = estadosCena.First.Value;
            estadosCena.RemoveFirst();
            return estadoSalvo;
        }
        return null;
    }


    public bool contemElementos()
    {
        return estadosCena.Count != 0;
    }

    public void limpaCenas()
    {
        estadosCena.Clear();
    }

}
