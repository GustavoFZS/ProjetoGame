using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEstrela : MonoBehaviour
{

    Vector2 posIni;
    Vector2 posFim;
    string[] mapa;
    int tamPasso;
    public LayerMask solido;

    public AEstrela(Vector2 posIni, Vector2 posFim, String[] mapa)
    {
        this.posIni = posIni;
        this.posFim = posFim;
        this.mapa = mapa;
        this.tamPasso = Controle.tamanhoCasas;
    }
    
    public Stack<Passo> getCaminho()
    {
        Stack<Passo> pilha = new Stack<Passo>();
        Passo atual = busca();

        //Inverte o caminho
        while (atual != null)
        {
            pilha.Push(atual);
            atual = atual.anterior;
        }

        return pilha;
    }

    Queue<Passo> getSucessores(Passo pos)
    {

        Queue<Passo> retorno = new Queue<Passo>();
        int[] aux = { 1, -1, 0, 0 };
        int x = pos.x;
        int y = pos.y;

        for (int i = 0; i < 4; i++)
        {
            if (!verificaColisaoLinha(new Vector2(x, y), new Vector2(x + tamPasso * aux[i], y + tamPasso * aux[(i + 2) % 4])))
            {
                retorno.Enqueue(new Passo(x + tamPasso * aux[i], y + tamPasso * aux[(i + 2) % 4], pos.peso + 1, pos));
            }

        }

        return retorno;

    }

    public Passo busca()
    {
        FilaDePrioridades<Passo> grafoBusca = new FilaDePrioridades<Passo>();
        Dictionary<Passo, int> visitados = new Dictionary<Passo, int>();
        grafoBusca.limpa();

        Passo inicio = new Passo((int)posIni.x, (int)posIni.y, 0, null);
        Passo fim = new Passo((int)posFim.x, (int)posFim.y, 0, null);
        grafoBusca.Add(inicio, 0);

        int teste = 0;

        while (grafoBusca.contemNos())
        {
            teste++;
            Passo atual = grafoBusca.get();

            if (atual.Equals(fim))
            {
                Debug.Log("Caminho encontrado, último ponto: " + atual + " / Nós visitados: " + teste);
                return atual;
            }

            Queue<Passo> retorno = getSucessores(atual);

            while (retorno.Count > 0)
            {
                Passo novo = retorno.Dequeue();
                int pesoTotal = novo.peso + Heuristica(novo, fim);

                if (!visitados.ContainsKey(novo))
                {
                    visitados[novo] = pesoTotal;
                    grafoBusca.Add(novo, pesoTotal);

                } else
                {
                    if (visitados[novo] > pesoTotal)
                    {
                        visitados[novo] = pesoTotal;
                        grafoBusca.Add(novo, pesoTotal);
                    }
                }
            }
        }

        Debug.Log("Não há uma rota para a posição: " + fim + " / Nós visitados: " + teste);
        return null;

    }

    int Heuristica(Passo pos, Passo objetivo)
    {
        return Mathf.Abs(pos.x - objetivo.x) + Mathf.Abs(pos.y - objetivo.y);
    }

    public bool verificaColisaoLinha(Vector3 fonte, Vector3 destino)
    {
        RaycastHit2D hit1 = Physics2D.Linecast(fonte, destino, solido);

        if (hit1.transform == null || hit1.transform.position.z > 1)
        {

            return false;

        }

        return true;

    }

}
