﻿using System.Collections.Generic;
using UnityEngine;

public class Controle : MonoBehaviour {

    static criadorDeMapas t = new criadorDeMapas();
    CenaCareTaker cenas = new CenaCareTaker();
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
                while (personagens.Count > 0)
                {
                    GameObject teste = Instantiate(personagens.First.Value);
                    teste.GetComponent<Principal>().setEstado(personagens.First.Value.GetComponent<Principal>().getEstado());
                    teste.SetActive(true);
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
