using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour {

    public static readonly int tamPasso = 1;

    class Passo : IComparable<Passo>
    {
        public float x;
        public float y;
        public int peso;
        public Passo anterior;

        public Passo(float x, float y, int peso, Passo anterior)
        {
            this.x = x;
            this.y = y;
            this.peso = peso;
            this.anterior = anterior;
        }

        public int CompareTo(Passo obj)
        {
            if (obj == null) return 1;

            if (this.peso == obj.peso) return 1;

            return this.peso - obj.peso;
        }

        public override string ToString()
        {
            return "X:" + x + " Y:" + y + " Peso:" + peso + " Anterior:" + anterior;
        }

    }

    SortedList<Passo, int> sorted;

    // Use this for initialization
    void Start () {

        sorted = new SortedList<Passo, int>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            FilaDePrioridades<char> teste = new FilaDePrioridades<char>();

            teste.Add('a', 2);
            Debug.Log(teste);

            teste.Add('v', 1);
            Debug.Log(teste);

            teste.Add('o', 0);
            Debug.Log(teste);

            teste.Add('u', 5);
            Debug.Log(teste);

            teste.Add('t', 3);
            Debug.Log(teste);

            teste.Add('s', 4);
            Debug.Log(teste);

            teste.Add('G', 6);
            Debug.Log(teste);

            teste.Add(' ', -1);
            Debug.Log(teste);

            teste.Add('F', -2);
            Debug.Log(teste);

            teste.Add('a', -3);
            Debug.Log(teste);

            teste.Add('z', -4);
            Debug.Log(teste);

            teste.Add('a', -5);
            Debug.Log(teste);

            teste.Add('n', -6);
            Debug.Log(teste);

            teste.Add('i', -7);
            Debug.Log(teste);

            teste.Add('X', -8);
            Debug.Log(teste);

            teste.Add('Y', -8);
            Debug.Log(teste);

            //Vector3 mousePos = getPosicaoMouseNaGrid();
            //aEstrela(mousePos);

        }

    }

    void OnMouseDown()
    {
		
		Debug.Log("Fui clicado");
		
    }

    Passo[] getSucessores(Passo pos)
    {

        Passo[] retorno = new Passo[4];
        float[] aux = {1,-1,0,0};
        float x = pos.x;
        float y = pos.y;

        for (int i = 0; i < 4; i++)
        {
            retorno[i] = new Passo(x + tamPasso * aux[i], y + tamPasso * aux[(i + 2) % 4], 0, null);
        }

        return retorno;

    }

    public void aEstrela(Vector3 pos)
    {

        Passo inicio = new Passo(pos.x, pos.y, 0, null);
        sorted.Add(inicio, 0);

        while (sorted.Count != 0)
        {
            Passo atual = sorted.Keys[0];

            Passo[] retorno = getSucessores(atual);
            for (int i = 0; i < retorno.Length; i++)
            {
                Debug.Log(retorno[i]);
                sorted.Add(retorno[i], i);
            }
            Debug.Log(sorted.Keys[0]);
        }

    }

    public Vector3 getPosicaoMouseNaGrid()
    {

        Vector3 vetorAux;

        vetorAux = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        vetorAux = new Vector3(Mathf.Round(vetorAux.x), Mathf.Round(vetorAux.y), vetorAux.z);

        return vetorAux;

    }

}
