using System.Collections.Generic;
using UnityEngine;

public class BuscaLargura : MonoBehaviour
{
    public static readonly int tamPasso = Controle.tamanhoCasas;

    static string[] mapa;
    static Transform prefab;
    static LayerMask solido;

    public static void init(Transform prefab, LayerMask solido)
    {
        BuscaLargura.prefab = prefab;
        BuscaLargura.solido = solido;
        mapa = Controle.getMapa();
    }

    static Queue<Passo> getSucessores(Passo pos, int tamBusca)
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

    public static Passo busca(Passo inicio, int largura)
    {
        Queue<Passo> grafoBusca = new Queue<Passo>();
        Queue<Passo> visitados = new Queue<Passo>();
        grafoBusca.Clear();

        grafoBusca.Enqueue(inicio);
        
        while (grafoBusca.Count > 0)
        {
            Passo atual = grafoBusca.Dequeue();
            Queue<Passo> retorno = getSucessores(atual, largura);
            while (retorno.Count > 0)
            {
                Passo novo = retorno.Dequeue();

                if (!visitados.Contains(novo))
                {
                    visitados.Enqueue(novo);
                    grafoBusca.Enqueue(novo);
                    criaEfeito(novo);
                }
            }

        }
        return null;
    }

    public static bool buscaOrientada(Passo inicio, Passo fim, int largura, bool recuar = false)
    {
        Queue<Passo> grafoBusca = new Queue<Passo>();
        Queue<Passo> visitados = new Queue<Passo>();
        grafoBusca.Clear();

        grafoBusca.Enqueue(inicio);

        while (grafoBusca.Count > 0)
        {
            Passo atual = grafoBusca.Dequeue();
            Queue<Passo> retorno = getSucessores(atual, largura);

            while (retorno.Count > 0)
            {
                Passo novo = retorno.Dequeue();
                
                if ((novo.Equals(fim) && !recuar) || (!novo.Equals(fim) && recuar))
                {
                    return true;
                }

                if (!visitados.Contains(novo))
                {
                    visitados.Enqueue(novo);
                    grafoBusca.Enqueue(novo);
                }
            }

        }
        return false;
    }

    static void criaEfeito(Passo novo)
    {
        var efeito = Instantiate(prefab, new Vector3(novo.x, novo.y, 1), Quaternion.identity);
        efeito.transform.localScale = new Vector3(Controle.tamanhoCasas, Controle.tamanhoCasas);
        efeito.tag = "AreaDeSelecao";
        efeito.GetComponent<Casa>().dono = Controle.selecionado.id;
    }

    public static bool verificaColisaoLinha(Vector3 fonte, Vector3 destino)
    {
        RaycastHit2D hit1 = Physics2D.Linecast(fonte, destino, solido);
        if (hit1.transform == null)
        {
            return false;
        }
        return true;
    }

}
