using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaLargura : MonoBehaviour
{

    int tamBusca;
    string[] mapa;
    Vector2 posIni;
    Vector2 posFim;

    public Transform prefab;
    public LayerMask solido;

    public BuscaLargura(Vector2 posIni, int tamBusca)
    {
        this.posIni = posIni;
        this.mapa = Controle.getMapa();
        this.tamBusca = tamBusca;
    }

    public static readonly int tamPasso = 1;

    Queue<Passo> getSucessores(Passo pos)
    {

        Queue<Passo> retorno = new Queue<Passo>();
        int[] aux = { 1, -1, 0, 0 };
        int x = pos.x;
        int y = pos.y;

        for (int i = 0; i < 4; i++)
        {
            if (!verificaColisaoLinha(new Vector2(x, y), new Vector2(x + tamPasso * aux[i], y + tamPasso * aux[(i + 2) % 4])) && (pos.peso + 1) < tamBusca)
            {
                retorno.Enqueue(new Passo(x + tamPasso * aux[i], y + tamPasso * aux[(i + 2) % 4], pos.peso + 1, pos));
            }

        }

        return retorno;

    }

    public Passo busca()
    {
        Queue<Passo> grafoBusca = new Queue<Passo>();
        Queue<Passo> visitados = new Queue<Passo>();
        grafoBusca.Clear();

        Passo inicio = new Passo((int)posIni.x, (int)posIni.y, 0, null);
        Passo fim = new Passo((int)posFim.x, (int)posFim.y, 0, null);
        grafoBusca.Enqueue(inicio);

        int teste = 0;

        while (grafoBusca.Count > 0)
        {
            Passo atual = grafoBusca.Dequeue();
            Queue<Passo> retorno = getSucessores(atual);
            teste++;

            while (retorno.Count > 0)
            {
                Passo novo = retorno.Dequeue();

                if (!visitados.Contains(novo))
                {
                    visitados.Enqueue(novo);
                    grafoBusca.Enqueue(novo);
                }
                Instantiate(prefab, new Vector3(novo.x, novo.y, 1), Quaternion.identity);
            }

        }
        return null;
    }

    public bool verificaColisaoLinha(Vector3 fonte, Vector3 destino)
    {
        RaycastHit2D hit1 = Physics2D.Linecast(fonte, destino, solido);

        if (hit1.transform == null)
        {

            return false;

        }

        return true;

    }

}
