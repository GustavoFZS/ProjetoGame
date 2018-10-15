using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controle : MonoBehaviour {

    bool cliqueBotaoTurno;
    public static float larguraMapa;
    public static float alturaMapa;
    public static int tamanhoCasas = 5;
    public static int turno = 0;
    public static int time = -1;
    public static float cameraAltura;
    public static float cameraLargura;
    public static Personagem selecionado;
    public static string ultimaMsg;

    static criadorDeMapas t = new criadorDeMapas();
    static CenaCareTaker cenas = new CenaCareTaker();
    public static Personagem escolhido;
    public Transform prefab;
    public LayerMask solido;
    public Transform paredes;
    public Transform piso;
    public Transform personagem;
    public Conexao conexao;
    static Personagem clicado1;
    static Personagem clicado2;
    static Maquina maquina;

    public static ClienteP2P cliente;
    public static ServidorP2P servidor;

    private void Awake()
    {
        cameraAltura = 2f * Camera.main.orthographicSize;
        cameraLargura = cameraAltura * Camera.main.aspect;
        t.criaMapa(paredes, piso, personagem);
        larguraMapa = getMapa()[0].Length * tamanhoCasas;
        alturaMapa = getMapa().Length * tamanhoCasas;
    }

    // Use this for initialization
    void Start () {
        if (Fluxo.tipoJogo == 1)
        {
            maquina = new Maquina();
        }
        if (Fluxo.tipoJogo == 2)
        {
            conexao = new Conexao();
            cliente = new ClienteP2P();
            servidor = new ServidorP2P();
        }
        turno = 1;
        mudaTurno(false);
        BuscaLargura.init(prefab, solido);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("PassaTurno") || cliqueBotaoTurno)
        {
            if (Metodos.checaAcao())
            {
                mudaTurno();
                if (Fluxo.tipoJogo == 2)
                {
                    cliente.EnviaTurnMsg();
                }
            }
        }

        if (Input.GetButtonDown("Volta"))
        {
            if (cenas.contemElementos())
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    Destroy(players[i]);
                }
                CenaMemento cenaAnterior = cenas.getUltimoEstadoSalvo();
                LinkedList<GameObject> personagens = cenaAnterior.getCena();
                turno = cenaAnterior.getTurno();
                while (personagens.Count > 0)
                {
                    GameObject original = Instantiate(personagens.First.Value);
                    original.GetComponent<Personagem>().mudaTurno();
                    original.SetActive(true);
                    personagens.RemoveFirst();
                }
                Debug.Log("Desfazer");
            }
        }

    }

    public static void setClicado(Personagem novo)
    {

        selecionado = novo;

        if (clicado1 == null)
        {
            clicado1 = novo;
            return;
        }

        if (clicado2 == null)
        {
            clicado2 = novo;
            return;
        }

        clicado1 = clicado2;
        clicado2 = novo;
    }

    public static void reiniciaClicados()
    {
        clicado1 = null;
        clicado2 = null;
    }

    public static bool clicouNoNada()
    {
        RaycastHit2D hit1 = Physics2D.Linecast(Metodos.getPosicaoMouseNaGrid(), Metodos.getPosicaoMouseNaGrid(), LayerMask.GetMask("solido"));

        if (hit1.transform == null)
        {
            return true;
        }
        return false;
    }

    public static Personagem getClicado1()
    {
        return clicado1;
    }

    public static Personagem getClicado2()
    {
        return clicado2;
    }

    public static string[] getMapa()
    {
        return t.mapa;
    }

    public static float getLarguraMapa()
    {
        return larguraMapa;
    }


    public static float getAlturaMapa()
    {
        return alturaMapa;
    }

    void OnGUI()
    {

        if (selecionado != null)
        {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 18;
            GUI.Box(new Rect(750, 400, 300, 350), selecionado.ToString(), myButtonStyle);
            
            if (turno == time)
            {
                cliqueBotaoTurno = GUI.Button(new Rect(750, 350, 300, 50), "Passar o turno", myButtonStyle);
            }
            else
            {
                cliqueBotaoTurno = GUI.Button(new Rect(750, 350, 300, 50), "Aguarde", myButtonStyle);
            }
        }
    }

    public static bool checaDistancia(float x, float y, int dist)
    {
        if (clicado2 == null)
            return false;
        return Mathf.Sqrt(Mathf.Pow(clicado2.transform.position.x - x, 2) + Mathf.Pow(clicado2.transform.position.y - y, 2)) < dist;
    }

    public static void mudaTurno(bool enviaMsg = true)
    {
        turno = turno == 0 ? 1 : 0;
        LinkedList<GameObject> players = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        LinkedList<GameObject> estadoAnterior = new LinkedList<GameObject>();
        LinkedList<GameObject>.Enumerator enu = players.GetEnumerator();
        while (enu.MoveNext())
        {
            enu.Current.GetComponent<Personagem>().mudaTurno();
            estadoAnterior.AddFirst(enu.Current.GetComponent<Personagem>().clone());
        }
        CenaMemento novaCena = new CenaMemento(estadoAnterior);
        cenas.adicionarMemento(novaCena);
    }

    public static void checaFimDoJogo(Personagem per)
    {
        LinkedList<GameObject> players = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        LinkedList<GameObject>.Enumerator enu = players.GetEnumerator();
        int pers1 = 0;
        int pers2 = 0;
        while (enu.MoveNext())
        {
            Personagem perAtual = enu.Current.GetComponent<Personagem>();
            if (perAtual == per)
            {
                continue;
            }
            if (perAtual.time == 0)
            {
                pers1++;
            }
            if (perAtual.time == 1)
            {
                pers2++;
            }
        }
        string msg = "Parabéns! Você ganhou";
        if ((pers1 == 0 && time == 0) || (pers2 == 0 && time == 1))
        {
            msg = "Derrota! Mais sorte na próxima";
        }
        if (pers1 == 0 && pers2 != 0)
        {
            SceneManager.LoadScene("Tela inicial", LoadSceneMode.Single);
            TelaInicial.msg = msg;
        }
        if (pers2 == 0 && pers1 != 0)
        {
            SceneManager.LoadScene("Tela inicial", LoadSceneMode.Single);
            TelaInicial.msg = msg;
        }
    }
}
