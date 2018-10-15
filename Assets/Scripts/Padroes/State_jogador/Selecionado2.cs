using UnityEngine;

public class Selecionado2 : State
{
    Personagem personagem;

    public Selecionado2(Personagem personagem)
    {
        this.personagem = personagem;
    }

    public override void executaAcao()
    {
        if (!Controle.clicouNoNada())
        {
            personagem.mudaBox();
            if (Controle.getClicado2() != null && BuscaLargura.buscaOrientada(personagem.toPasso(), Controle.getClicado2().toPasso(), personagem.alcance))
            {
                if (Controle.getClicado2().setMensagem(personagem))
                {
                    Controle.reiniciaClicados();
                    novoEstado();
                }
            }
            else
            {
                voltaEstado();
            }
            personagem.mudaBox();
        }
    }

    public override bool useHab(string novaHab)
    {
        personagem.setHabilidade(novaHab);
        return true;
    }

    public override bool useMouse()
    {
        return true;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Indisponivel(personagem));
    }

    void voltaEstado()
    {
        personagem.setEstado(new PodeAtacar(personagem));
    }

}
