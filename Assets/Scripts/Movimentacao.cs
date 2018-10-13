using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour {

    public Transform prefab;
    public LayerMask solido;

    public IEnumerator Mover(int maxMov, Vector2 posicao)
    {
        AEstrela buscaCaminho = new AEstrela(new Vector2(transform.position.x, transform.position.y), posicao, maxMov, Controle.getMapa());
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

    public void mostraRota(int mov)
    {
        BuscaLargura.busca(new Passo((int) transform.position.x, (int) transform.position.y), mov);
    }

}
