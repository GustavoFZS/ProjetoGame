using System.Collections.Generic;
using UnityEngine;

public class CenaMemento {

    LinkedList<GameObject> personagens = new LinkedList<GameObject>();
    int turno;

    public CenaMemento(LinkedList<GameObject> personagens)
    {
        this.personagens = personagens;
        turno = Controle.turno;
    }

    public LinkedList<GameObject> getCena()
    {
        return personagens;
    }

    public int getTurno()
    {
        return turno;
    }

}
