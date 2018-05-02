using System;
using UnityEngine;

public class InterpretadorPadrao : Interpretador
{

    public override void recebeMensagem(Principal destino, string mensagem)
    {
        char[] quebras = { '|', ','};
        string[] expressao = mensagem.Split(quebras[0]);
        string efeito = expressao[0];
        string[] valores = expressao[1].Split(quebras[1]);

        destino.adiconaEfeito(efeito);

        int vida = Mathf.RoundToInt(float.Parse(valores[0]) * (1f - destino.defesa));
        int ataque = validaValor(1, valores);
        int alcance = validaValor(2, valores); ;
        int movimentacao = validaValor(3, valores);

        switch (efeito)
        {
            case "Veneno":
                break;
            case "Acido":
                break;
            case "Cura":
                break;
            case "Escudo":
                break;
            case "Fogo":
                break;
        }

        destino.vida += vida;
        destino.ataque += ataque;
        destino.alcance += alcance;
        destino.movimentacao += movimentacao;

    }

    int validaValor(int i, string[] valores)
    {
        return valores.Length > i ? Int32.Parse(valores[i]) : 0;
    }
}
