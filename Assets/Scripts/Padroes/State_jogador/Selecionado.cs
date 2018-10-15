using UnityEngine;

public class Selecionado : State
{
    Personagem personagem;

    public Selecionado(Personagem personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        personagem.GetComponent<Movimentacao>().mostraRota(personagem.movimentacao);
        personagem.mudaBox();
    }

    public override void executaAcao()
    {
        personagem.apagaRotas();

        if (Controle.clicouNoNada())
        { 
            if (personagem.anda(Metodos.getPosicaoMouseNaGrid()))
            {
                novoEstado();
            }
            else
            {
                voltaEstado();
            }
        }
        else
        {
            personagem.mudaBox();
            if (Controle.getClicado2() != null && BuscaLargura.buscaOrientada(personagem.toPasso(), Controle.getClicado2().toPasso(), personagem.alcance))
            {
                if (Controle.getClicado2().setMensagem(personagem))
                {
                    Controle.reiniciaClicados();
                    finaliza();
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
        personagem.setEstado(new PodeAtacar(personagem));
    }

    void finaliza()
    {
        personagem.setEstado(new Indisponivel(personagem));
    }

    void voltaEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }
}
