using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Maquina : MonoBehaviour
{
    bool bloq;

    GameObject[] players;
    LinkedList<Personagem> personagens;

    void Start()
    {
        bloq = false;
    }

    void Update()
    {
        Debug.Log("Eu existo");
        if(Controle.turno == 1 && !bloq)
        {
            bloq = true;
            players = GameObject.FindGameObjectsWithTag("Player");
            personagens = new LinkedList<Personagem>();

            for (int i = 0; i < players.Length; i++)
            {
                Personagem aux = players[i].GetComponent<Personagem>();
                if (aux.time == 1)
                {
                    personagens.AddFirst(aux);
                }
            }
            StartCoroutine(proximo());
        }
    }

    public IEnumerator proximo()
    {
        while (personagens.Count > 0)
        {
            Personagem proximo = personagens.First.Value;
            personagens.RemoveFirst();
            //ArvoreDeComportamento.getAcao(proximo);
            yield return new WaitForSeconds(0.5f);
        }
        Controle.mudaTurno();
        bloq = false;
    }

}
