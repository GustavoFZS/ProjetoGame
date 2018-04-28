﻿using UnityEngine;

public class Controle : MonoBehaviour {

    static criadorDeMapas t = new criadorDeMapas();
    static float larguraMapa;
    static float alturaMapa;

    public Transform paredes;
    public Transform personagem;

    private void Awake()
    {
        t.criaMapa(paredes, personagem);
        larguraMapa = getMapa()[0].Length;
        alturaMapa = getMapa().Length;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
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
}