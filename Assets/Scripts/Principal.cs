using System;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour {

    public int vida;
    public int ataque;
    public int alcance;
    public int movimentacao;
    public int time;
    public float defesa;

    bool mouseFora = false;
    string habilidade;
    BoxCollider2D box;
    Dictionary<string, int> efeitos = new Dictionary<string, int>();
    Interpretador interpretador = new InterpretadorPadrao();
    State estado;

    // Use this for initialization
    void Start() {
        box = GetComponent<BoxCollider2D>();
        vida = 120;
        defesa = 0.2f;
        ataque = 12;
        alcance = 7;
        movimentacao = 5;
        habilidade = "padrao";
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0) && mouseFora && estado.useMouse())
        {
            estado.executaAcao();
        }

    }

    void OnMouseOver()
    {
        mouseFora = false;
    }

    void OnMouseExit()
    {
        mouseFora = true;
    }

    void OnMouseDown()
    {
        Controle.selecionado = this;
        estado.clicado();
    }

    public void adiconaEfeito(string novoEfeito)
    {
        if (efeitos.ContainsKey(novoEfeito))
        {
            efeitos[novoEfeito]++;
            return;
        }
        efeitos.Add(novoEfeito, 1);
    }

    public string getHabilidade()
    {
        return habilidade + "|-" + ataque;
    }

    public void mudaBox()
    {
        box.enabled = !box.enabled;
    }

    public void apagaRotas()
    {
        var areasDeSelecao = GameObject.FindGameObjectsWithTag("AreaDeSelecao");
        for (int i = 0; i < areasDeSelecao.Length; i++)
        {
            Destroy(areasDeSelecao[i]);
        }
    }

    public bool anda()
    {
        mudaBox();
        if (StartCoroutine(GetComponent<Movimentacao>().Mover()) == null)
        {
            apagaRotas();
            mudaBox();
            return false;
        }
        apagaRotas();
        mudaBox();
        return true;
    }

    public void clicado()
    {
        Controle.escolhido = this;
        estado.clicado();
    }

    public void setEstado(State estado)
    {
        this.estado = estado;
    }

    public State getEstado()
    {
        return this.estado;
    }

    public void setMensagem(string comando)
    {
        interpretador.recebeMensagem(this, comando);
    }

    public void mudaTurno()
    {
        if (Controle.turno == time)
        {
            setEstado(new PodeAndaEAtacar(this));
            return;
        }
        setEstado(new Indisponivel(this));
    }

    public GameObject clone()
    {
        GameObject clone = new GameObject();

        clone.AddComponent<BoxCollider2D>();
        clone.AddComponent<Movimentacao>();
        clone.AddComponent<Principal>();

        Movimentacao cloneMovimentacao = clone.GetComponent<Movimentacao>();
        SpriteRenderer cloneSpriteRenderer = clone.AddComponent<SpriteRenderer>();
        Principal clonePrincipal = clone.GetComponent<Principal>();

        cloneMovimentacao.prefab = GetComponent<Movimentacao>().prefab;
        cloneMovimentacao.solido = GetComponent<Movimentacao>().solido;

        clonePrincipal.Start();

        Vector3 posicao = transform.position;

        clone.transform.position = new Vector3(posicao.x, posicao.y, posicao.z);
        cloneSpriteRenderer.sprite = this.GetComponent<SpriteRenderer>().sprite;

        clonePrincipal.vida = vida;
        clonePrincipal.defesa = defesa;
        clonePrincipal.ataque = ataque;
        clonePrincipal.alcance = alcance;
        clonePrincipal.movimentacao = movimentacao;
        clonePrincipal.habilidade = habilidade;
        clonePrincipal.time = time;
        clonePrincipal.interpretador = interpretador.clone();
 
        Dictionary<string, int> cloneEfeitos = new Dictionary<string, int>();

        foreach (KeyValuePair<string, int> efeito in clonePrincipal.efeitos)
        {
            cloneEfeitos.Add(efeito.Key, efeito.Value);
        }
 
        clonePrincipal.efeitos = cloneEfeitos;

        clone.SetActive(false);
        clone.gameObject.tag = "Player";
        clone.gameObject.layer = 8;

        return clone;
    }

    public override string ToString()
    {
        return "Ataque: " + ataque + "  Vida: " + vida + " Alcance: " + alcance + " Movimentacao: " + movimentacao;
    }

}

