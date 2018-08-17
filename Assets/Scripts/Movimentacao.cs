using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour {

    public Transform prefab;
    public LayerMask solido;

    public IEnumerator Mover()
    {
        AEstrela buscaCaminho = new AEstrela(new Vector2(transform.position.x, transform.position.y), getPosicaoMouseNaGrid(), Controle.getMapa());
        buscaCaminho.solido = solido;
        Stack<Passo> pilha = buscaCaminho.getCaminho();
        Passo atual;

        while (pilha.Count != 0)
        {
            atual = pilha.Pop();
            Vector3 novaPos = new Vector2(atual.x, atual.y);
            transform.position = novaPos;
            atual = atual.anterior;
            yield return new WaitForSeconds(0.08f);
        }
    }

    public void mostraRota()
    {

        BuscaLargura buscaCaminho = new BuscaLargura(new Vector2(transform.position.x, transform.position.y), 5);
        buscaCaminho.prefab = prefab;
        buscaCaminho.solido = solido;
        buscaCaminho.busca();

    }

    public Vector3 getPosicaoMouseNaGrid()
    {

        Vector2 vetorAux;

        vetorAux = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        vetorAux = new Vector2(Mathf.Round(vetorAux.x/Controle.tamanhoCasas)*Controle.tamanhoCasas, Mathf.Round(vetorAux.y/Controle.tamanhoCasas) *Controle.tamanhoCasas);

        return vetorAux;

    }

}
