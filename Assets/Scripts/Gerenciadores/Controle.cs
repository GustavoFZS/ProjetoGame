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
    public static Principal escolhido;
    public static Principal selecionado;

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
        
        if (Input.GetButtonDown("PassaTurno"))
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
                    original.GetComponent<Principal>().mudaTurno();
                    original.SetActive(true);
                    personagens.RemoveFirst();
                }
                Debug.Log("Desfazer");
            }
        }

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
        if (escolhido != null)
        {
            GUI.Box(new Rect(10, 320, 400, 200), selecionado.ToString());
        }
    }

    public static string getAtaqueInimigo()
    {
        return escolhido.getHabilidade();
    }

    public static bool checaAcao(int time)
    {
        if (escolhido == null)
            return true;
        return time == escolhido.time;
    }

    void mudaTurno()
    {
        turno = turno == 0 ? 1 : 0;
        LinkedList<GameObject> players = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        LinkedList<GameObject> estadoAnterior = new LinkedList<GameObject>();
        LinkedList<GameObject>.Enumerator enu = players.GetEnumerator();
        while (enu.MoveNext())
        {
            enu.Current.GetComponent<Principal>().mudaTurno();
            estadoAnterior.AddFirst(enu.Current.GetComponent<Principal>().clone());
        }
        CenaMemento novaCena = new CenaMemento(estadoAnterior);
        cenas.adicionarMemento(novaCena);
    }
}
