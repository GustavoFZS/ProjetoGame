﻿using UnityEngine;

public class FabricaDePisos : MonoBehaviour
{

    Transform solido;
    Transform paisagem;

    Sprite casaP, casaB, piso, obs, imagem;
    int tipo = 0;

    public FabricaDePisos(Transform solido, Transform paisagem)
    {
        this.solido = solido;
        this.paisagem = paisagem;
        casaP = Resources.Load<Sprite>("CasaP");
        casaB = Resources.Load<Sprite>("CasaB");
        piso = Resources.Load<Sprite>("Piso");
        obs = Resources.Load<Sprite>("obstaculo");

    }

    public Transform criaPiso(int camada, int tipo, bool ehSolido, float x, float y)
    {
        Transform retorno;

        if (ehSolido)
        {
            retorno = Instantiate(solido, new Vector3(x, y, camada), Quaternion.identity);
        }
        else
        {
            retorno = Instantiate(paisagem, new Vector3(x, y, camada), Quaternion.identity);
        }

        switch (tipo)
        {
            case 0:
                imagem = casaP;
                break;
            case 1:
                imagem = casaB;
                break;
            case 2:
                imagem = piso;
                break;
            case 3:
                imagem = obs;
                break;
        }

        retorno.transform.localScale = new Vector3(Controle.tamanhoCasas, Controle.tamanhoCasas);
        retorno.GetComponent<SpriteRenderer>().sprite = imagem;
        return retorno;
    }

}