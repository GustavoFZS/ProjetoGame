using System.Collections.Generic;
using UnityEngine;

public class Controle : MonoBehaviour {

    static criadorDeMapas t = new criadorDeMapas();
    CenaCareTaker cenas = new CenaCareTaker();
    public static float larguraMapa;
    public static float alturaMapa;
    public static int tamanhoCasas = 5;
    public static int turno;
    public static float cameraAltura;
    public static float cameraLargura;

    public Transform paredes;
    public Transform piso;
    public Transform personagem;
    public static Personagem escolhido;

    static Personagem clicado1;
    static Personagem clicado2;
    static Personagem selecionado;

    bool cliqueBotaoTurno;

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
        turno = 1;
        mudaTurno();
    }
	
	// Update is called once per frame
	void Update () {
                
        if (Input.GetButtonDown("PassaTurno") || cliqueBotaoTurno)
        {
            mudaTurno();
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

    public static bool cliqueDuplo()
    {
        return clicado1 == clicado2;
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
            
            if (turno == 0)
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

    void mudaTurno()
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
}
