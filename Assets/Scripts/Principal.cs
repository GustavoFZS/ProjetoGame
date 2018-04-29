using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour {

    public int vida;
    public int ataque;
    public int alcance;
    public int movimentacao;
    public int time;
    public float defesa;

    bool mouseDentro = false;
    string habilidade;
    BoxCollider2D box;
    Dictionary<string, int> efeitos = new Dictionary<string, int>();
    Interpretador interp;
    State estado;

    // Use this for initialization
    void Start () {
        box = GetComponent<BoxCollider2D>();
        vida = 12;
        defesa = 0.2f;
        ataque = 12;
        alcance = 7;
        movimentacao = 5;
        habilidade = "padrao";
        interp = new InterpretadorPadrao();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && mouseDentro)
        {
            estado.executaAcao();
        }

    }

    void OnMouseOver()
    {
        mouseDentro = false;
    }

    void OnMouseExit()
    {
        mouseDentro = true;
    }

    void OnMouseDown()
    {
        Controle.selecionado = this;
        estado.clicado();
    }

    public override string ToString()
    {
        return "Ataque: " + ataque + "  Vida: " + vida + " Alcance: " + alcance + " Movimentacao: " + movimentacao;
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

    public void setMensagem(string comando)
    {
        interp.recebeMensagem(this, comando);
    }

    public void mudaTurno()
    {
        if(Controle.turno == time)
        {
            setEstado(new PodeAndaEAtacar(this));
            return;
        }
        setEstado(new Indisponivel(this));
    }

}
