using System;
using UnityEngine;

// |Nome da habilidade|
// |Tipo (0: usa em aliado, 1: usa em inimigo, 2: usa em si mesmo)|
// |Modificador de alcance|
// |Dano|
// |Redução de ataque|
// |Redução de defesa|
// |Redução de alcance|
// |Redução de movimentacao|
public class InterpretadorPadrao : Interpretador
{

    public override bool recebeMensagem(Personagem destino, Personagem emissor, Transform prefab)
    {

        string mensagem = emissor.getHabilidade();
        bool ehAliado = emissor.time == destino.time;

        char[] quebras = { '|', ','};
        string[] expressao = mensagem.Split(quebras[0]);
        string nome = expressao[0];
        string tipo = expressao[1];
        string modAlcance = expressao[2];
        string[] valores = expressao[3].Split(quebras[1]);

        destino.adiconaEfeito(nome);

        //Debug.Log(-int.Parse(valores[0]));
        //Debug.Log(destino.defesa);
        //Debug.Log(calcDano(-int.Parse(valores[0]), destino.defesa));

        int vida = Mathf.RoundToInt(calcDano(-float.Parse(valores[0]), destino.defesa));
        int ataque = validaValor(1, valores);
        int defesa = validaValor(2, valores);
        int alcance = validaValor(3, valores);
        int movimentacao = validaValor(4, valores);

        if ((tipo.Equals("0") && !ehAliado) || (tipo.Equals("1") && ehAliado))
        {
            return false;
        }

        switch (nome)
        {
            case "Padrao":
                break;
            case "Veneno":
                break;
            case "Acido":
                break;
            case "Cura":
                Historico.recebeValor(emissor.nome + " usou Cura");
                vida = 50;
                break;
            case "Escudo":
                break;
            case "Fúria":
                Historico.recebeValor(emissor.nome + " usou Fúria");
                vida = Mathf.RoundToInt(calcDano(-float.Parse(valores[0])*2, destino.defesa));
                break;
        }

        destino.vida += vida;
        destino.defesa += defesa;
        destino.ataque += ataque;
        destino.alcance += alcance;
        destino.movimentacao += movimentacao;

        if (vida < 0)
        {
            Historico.recebeValor(destino.nome + " perdeu " + vida + " de vida");
        }
        else
        {
            Historico.recebeValor(destino.nome + " ganhou " + vida + " de vida");
        }

        return true;
    }

    int validaValor(int i, string[] valores)
    {
        return valores.Length > i ? Int32.Parse(valores[i]) : 0;
    }

    float calcDano(float atk, int def)
    {
        return - Mathf.Max(0, Mathf.RoundToInt((2 * atk - def)/2));
    }
}
