using UnityEngine;

public class Controle : MonoBehaviour {

    static criadorDeMapas t = new criadorDeMapas();
    static float larguraMapa;
    static float alturaMapa;
    public static int turno;

    public Transform paredes;
    public Transform personagem;
    public static Principal escolhido;
    public static Principal selecionado;

    private void Awake()
    {
        t.criaMapa(paredes, personagem);
        larguraMapa = getMapa()[0].Length;
        alturaMapa = getMapa().Length;
    }

    // Use this for initialization
    void Start () {
        turno = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Principal>().mudaTurno();
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("PassaTurno"))
        {
            turno = turno == 0 ? 1 : 0;
            Debug.Log("Troca de turno: " + turno);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<Principal>().mudaTurno();
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
}
