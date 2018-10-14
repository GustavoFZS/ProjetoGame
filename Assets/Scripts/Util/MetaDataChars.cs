using UnityEngine;

public class MetaDataChars
{
    public int vida, ataque, alcance, movimentacao, defesa;
    public Sprite sprite;
    public string nome, habPadrao, hab1, hab2;

    public MetaDataChars(int vida, int ataque, int defesa, int alcance, int movimentacao, Sprite sprite, string nome, string habPadrao, string hab1, string hab2)
    {
        this.vida = vida;
        this.ataque = ataque;
        this.defesa = defesa;
        this.alcance = alcance;
        this.habPadrao = habPadrao;
        this.hab1 = hab1;
        this.hab2 = hab2;
        this.movimentacao = movimentacao;
        this.sprite = sprite;
        this.nome = nome;
    }
}