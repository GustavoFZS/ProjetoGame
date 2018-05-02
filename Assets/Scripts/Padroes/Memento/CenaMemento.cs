using System.Collections.Generic;
using UnityEngine;

public class CenaMemento {

    LinkedList<GameObject> personagens = new LinkedList<GameObject>();

    public CenaMemento(LinkedList<GameObject> personagens)
    {
        this.personagens = personagens; 
    }

    public LinkedList<GameObject> getCena()
    {
        return personagens;
    }

}
