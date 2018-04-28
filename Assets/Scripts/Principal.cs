using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : MonoBehaviour {

    bool selecionado = false;
    BoxCollider2D box;

    // Use this for initialization
    void Start () {
        box = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (selecionado && Input.GetMouseButtonDown(0))
        {
            box.enabled = false;
            StartCoroutine(GetComponent<Movimentacao>().Mover());
            selecionado = false;
            var areasDeSelecao = GameObject.FindGameObjectsWithTag("AreaDeSelecao");
            for (int i = 0; i < areasDeSelecao.Length; i++)
            {
                Destroy(areasDeSelecao[i]);
            }
            box.enabled = true;
        }

    }

    void OnMouseUp()
    {
        box.enabled = false;
        GetComponent<Movimentacao>().mostraRota();
        selecionado = true;
        box.enabled = true;
    }

}
