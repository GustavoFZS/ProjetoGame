using UnityEngine;
using System.Collections.Generic;

public class ArvoreDeComportamento : MonoBehaviour
{
    public static int atacante = 0;
    public static int defensor = 1;
    public static int suporte = 2;

    static LinkedList<Passo> geraPassos(LinkedList<Personagem> personagens)
    {
        LinkedList<Personagem>.Enumerator enu = personagens.GetEnumerator();
        LinkedList<Passo> passos = new LinkedList<Passo>();

        while (enu.MoveNext())
        {
            passos.AddFirst(enu.Current.toPasso());
        }

        return passos;
    }

}
