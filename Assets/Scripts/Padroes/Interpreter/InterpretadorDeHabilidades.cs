using System;
using UnityEngine;
using System.Collections.Generic;

// |Nome da habilidade|
// |Tipo (0: usa em aliado, 1: usa em inimigo, 2: usa em si mesmo)|
// |Modificador de alcance|
// |Dano|
// |Redução de ataque|
// |Redução de defesa|
// |Redução de alcance|
// |Redução de movimentacao|
public class InterpretadorDeHabilidades : Interpretador
{
    char[] quebras = { '%', '|', '-' };

    public override bool recebeMensagem(Personagem destino, Personagem emissor, string habilidade, bool local = true)
    {
        string mensagem;
        bool ehAuto, ehAliado;

        if (destino == null)
        {
            destino = emissor;
            mensagem = habilidade;
            ehAuto = true;
            ehAliado = false;
        }
        else
        {
            mensagem = emissor.getHabilidade();
            ehAuto = emissor == destino;
            ehAliado = (emissor.time == destino.time) && !ehAuto;
        }
        
        string[] nome_tuplas = mensagem.Split(quebras[0]);
        string[] tuplas = nome_tuplas[1].Split(quebras[1]);
        string nome = nome_tuplas[0];
        Dictionary<string, int> valores = new Dictionary<string, int>();
        
        foreach (string tupla in tuplas)
        {
            string[] chave_valor = tupla.Split(quebras[2]);
            valores.Add(chave_valor[0], Int32.Parse(chave_valor[1]));
        }

        //Casos inválidos
        if ((valores["tipo"].Equals(0) && (ehAliado || ehAuto)) || (valores["tipo"].Equals(1) && !ehAliado)
            || (valores["tipo"].Equals(2) && !(ehAliado || ehAuto)) || (valores["tipo"].Equals(3) && !ehAuto))
        {
            return false;
        }

        if (Fluxo.tipoJogo == 2 && local)
        {
           Controle.cliente.EnviaHabMsg(destino.id, emissor.id, mensagem);
        }

        return caculador(destino, emissor, valores, nome);
    }

    bool caculador(Personagem destino, Personagem emissor, Dictionary<string, int> valores, string nome)
    {

        int dano = calcDanoPadrao(emissor.ataque, destino.defesa);
        switch (nome)
        {
            case "Padrao":
                destino.vida -= dano;
                valores.Add("dano", dano);
                break;
            case "Veneno":
                break;
            case "Acido":
                break;
            case "Cura":
                break;
            case "Escudo":
                break;
            case "Fúria":
                dano = calcDanoPadrao(emissor.ataque * 2, destino.defesa);
                destino.vida -= dano;
                valores.Add("dano", dano);
                break;
        }

        if (!nome.Equals("Padrao"))
        {
            Historico.recebeValor(emissor.nome + " usou " + nome + ".");
            emissor.resetaHad();
        }

        string[] campos = { "vida", "defesa", "ataque", "alcance", "movimentacao",
                            "vida2", "defesa2", "ataque2", "alcance2", "movimentacao2" };

        foreach (string campo in campos)
        {
            if (valores.ContainsKey(campo))
            {
                if (valores.ContainsKey(campo + "_duracao"))
                {
                    destino.adiconaEfeito(efeitoEmCodigo(nome, campo, valores[campo], quebras), valores[campo + "_duracao"]);
                }
            }
        }

        destino.vida += valores.ContainsKey("vida") ? valores["vida"] : 0;
        destino.defesa += valores.ContainsKey("defesa") ? valores["defesa"] : 0;
        destino.ataque += valores.ContainsKey("ataque") ? valores["ataque"] : 0;
        destino.alcance += valores.ContainsKey("alcance") ? valores["alcance"] : 0;
        destino.movimentacao += valores.ContainsKey("movimentacao") ? valores["movimentacao"] : 0;

        destino.vida = valores.ContainsKey("vida2") ? valores["vida2"] * destino.vida : destino.vida;
        destino.defesa = valores.ContainsKey("defesa2") ? valores["defesa2"] * destino.defesa : destino.defesa;
        destino.ataque = valores.ContainsKey("ataque2") ? valores["ataque2"] * destino.ataque : destino.ataque;
        destino.alcance = valores.ContainsKey("alcance2") ? valores["alcance2"] * destino.alcance : destino.alcance;
        destino.movimentacao = valores.ContainsKey("movimentacao2") ? valores["movimentacao2"] * destino.movimentacao : destino.movimentacao;

        if (valores.ContainsKey("dano"))
        {
            if(valores["dano"] >= 0)
            {
                Historico.recebeValor(destino.nome + " perdeu " + valores["dano"] + " de vida");
            }
            else
            {
                Historico.recebeValor(destino.nome + " ganhou " + valores["dano"] + " de vida");
            }
        }

        return true;
    }

    string efeitoEmCodigo(string nome, string campo, int valor, char[] quebras)
    {
        return nome + quebras[0] + "tipo" + quebras[2] + "3" + quebras[1] + campo + quebras[2] + valor;
    }

    int calcDanoPadrao(float atk, int def)
    {
        return Mathf.Max(0, Mathf.RoundToInt((4 * atk - def)/12 * (1 + (def/atk))));
    }
}
