public class Selecionado : State
{
    Personagem personagem;

    public Selecionado(Personagem personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        Controle.setClicado(personagem);
        personagem.GetComponent<Movimentacao>().mostraRota(personagem.movimentacao);
        personagem.mudaBox();
    }

    public override void executaAcao()
    {
        personagem.apagaRotas();

        if (Controle.clicouNoNada())
        { 
            if (personagem.anda())
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
            BuscaLargura buscaCaminho = new BuscaLargura(personagem.transform.position.x, personagem.transform.position.y, Controle.getClicado2().transform.position.x, Controle.getClicado2().transform.position.y, personagem.alcance);

            if (buscaCaminho.busca2())
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
        personagem.setEstado(new Selecionado2(personagem));
    }

    void finaliza()
    {
        personagem.setEstado(new Indisponivel(personagem));
    }

    public override void clicado()
    {
        Controle.setClicado(personagem);
    }

    void voltaEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }
}
