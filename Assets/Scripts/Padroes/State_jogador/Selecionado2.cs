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
            //if (BuscaLargura.busca2())
            //{
                if (Controle.getClicado2().setMensagem(personagem))
                {
                    Controle.reiniciaClicados();
                    novoEstado();
                }
            //}
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

}
