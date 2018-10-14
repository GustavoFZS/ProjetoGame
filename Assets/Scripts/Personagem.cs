using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour {

    public int personalidade;

    public string id;

    public int vida;
    public int ataque;
    public int alcance;
    public int movimentacao;
    public int time;
    public int defesa;
    public int energia;
    public string nome;

    public int vidaInicial;
    public int ataqueInicial;
    public int alcanceInicial;
    public int movimentacaoInicial;
    public int defesaInicial;
    public int energiaInicial;

    public Texture aTexture;

    bool mouseFora = false;
    string habilidade;
    string habPadrao;
    string especial1;
    string especial2;
    BoxCollider2D box;
    Dictionary<string, int> efeitos = new Dictionary<string, int>();
    State estado;
    Interpretador interpretador = new InterpretadorDeHabilidades();

    void Start() {
        box = GetComponent<BoxCollider2D>();
        habilidade = habPadrao;
        id = (transform.position.x + "" + transform.position.y*-1);
    }

    void Update() {

        if (Input.GetMouseButtonDown(0) && mouseFora && estado.useMouse() && Metodos.checaAcao())
        {
            estado.executaAcao();
        }

        if (estado.useMouse() && Metodos.checaAcao())
        {
            if (Input.GetButtonDown("Hab1"))
            {
                estado.useHab(especial1);
                interpretador.recebeMensagem(this, this, "");
            }

            if (Input.GetButtonDown("Hab2"))
            {
                estado.useHab(especial2);
                interpretador.recebeMensagem(this, this, "");
            }
        }

        if (vida < 1)
        {
            Destroy(gameObject);
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
        Controle.setClicado(this);
        if (Metodos.checaAcao())
        {
            estado.clicado();
        }
    }

    public void adiconaEfeito(string novoEfeito, int duracao)
    {
        if (efeitos.ContainsKey(novoEfeito))
        {
            efeitos[novoEfeito]+= duracao;
            return;
        }
        efeitos.Add(novoEfeito, duracao);
    }

    public void aplicaEfeitos()
    {
        List<string> keys = new List<string>(efeitos.Keys);
        foreach (string efeito in keys)
        {
            if (efeitos[efeito] > 0)
            {
                interpretador.recebeMensagem(null, this, efeito);
                efeitos[efeito]--;
            }
        }
    }

    public string getHabilidade()
    {
        return habilidade;
    }

    public void setHabilidade(string novaHab)
    {
        habilidade = novaHab;
    }

    public void mudaBox()
    {
        box.enabled = !box.enabled;
    }

    public void apagaRotas()
    {
        GameObject[] areasDeSelecao = GameObject.FindGameObjectsWithTag("AreaDeSelecao");
        for (int i = 0; i < areasDeSelecao.Length; i++)
        {
            if (areasDeSelecao[i].GetComponent<Casa>().dono.Equals(id))
            {
                Destroy(areasDeSelecao[i]);
            }
        }
    }

    public bool anda(Vector2 posicao, bool mandaMsg = false)
    {

        if (Fluxo.tipoJogo == 2 && !mandaMsg)
        {
            Controle.cliente.EnviaMovMsg(id, posicao.x, posicao.y);
        }

        mudaBox();
        if (StartCoroutine(GetComponent<Movimentacao>().Mover(movimentacao, posicao)) == null)
        {
            mudaBox();
            return false;
        }
        mudaBox();
        return true;
    }

    public void setEstado(State estado)
    {
        this.estado = estado;
    }

    public State getEstado()
    {
        return estado;
    }

    public bool setMensagem(Personagem emissor)
    {
        return interpretador.recebeMensagem(this, emissor, "");
    }

    public void mudaTurno()
    {
        if (Controle.turno == time)
        {
            energia = Mathf.Min(energia + 15, energiaInicial);
            aplicaEfeitos();
            if (time == 0 || Fluxo.tipoJogo != 1)
            {
                setEstado(new PodeAndaEAtacar(this));
            }
            else
            {
                setEstado(new Disponivel(this));
            }
            return;
        }
        if (time == 0 || Fluxo.tipoJogo != 1)
        {
            setEstado(new Indisponivel(this));
        }
        else
        {
            setEstado(new Indisponivel2(this));
        }
    }

    public void setaAtributos(int vida, int ataque, int alcance, int movimentacao, int defesa, int time, string nome, string habP, string hab1, string hab2)
    {
        this.vida = vida;
        this.ataque = ataque;
        this.alcance = alcance;
        this.movimentacao = movimentacao;
        this.defesa = defesa;
        this.energia = 100;

        this.vidaInicial = vida;
        this.ataqueInicial = ataque;
        this.alcanceInicial = alcance;
        this.movimentacaoInicial = movimentacao;
        this.defesaInicial = defesa;
        this.energiaInicial = 100;
        this.habPadrao = habP;
        this.especial1 = hab1;
        this.especial2 = hab2;

        this.habilidade = habP;

        this.time = time;
        this.nome = nome;
    }

    public GameObject clone()
    {
        GameObject clone = new GameObject();

        clone.AddComponent<BoxCollider2D>();
        clone.AddComponent<Movimentacao>();
        clone.AddComponent<Personagem>();

        Movimentacao cloneMovimentacao = clone.GetComponent<Movimentacao>();
        SpriteRenderer cloneSpriteRenderer = clone.AddComponent<SpriteRenderer>();
        Personagem clonePrincipal = clone.GetComponent<Personagem>();

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
        clonePrincipal.energia = energia;
        clonePrincipal.nome = nome;
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
        return nome + "\r\n\r\n\r\n" +  "Vida: " + vida + "\r\n\r\n" + "Ataque: " + ataque + "\r\n\r\n" + "Defesa: " + defesa + "\r\n\r\n" + " Alcance: " + (alcance - 1) + "\r\n\r\n" + "Movimentacao: " + movimentacao;
    }

    public Passo toPasso()
    {
        return new Passo((int) transform.position.x,(int) transform.position.y, 0, null);
    }

    void OnGUI()
    {
        Vector2 posicaoTela = Camera.main.WorldToScreenPoint(transform.position);
        float aux = Screen.height - posicaoTela.y;
        GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
        myLabelStyle.fontSize = 18;
        GUI.Label(new Rect(posicaoTela.x + 50, aux + 60, 100, 100), "" + vida, myLabelStyle);
    }

}

