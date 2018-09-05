public class Selecionado2 : State
{
    Personagem personagem;

    public Selecionado2(Personagem personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        Controle.setClicado(personagem);
        personagem.mudaBox();
    }

    public override void executaAcao()
    {
        if (!Controle.clicouNoNada())
        {
            BuscaLargura buscaCaminho = new BuscaLargura(personagem.transform.position.x, personagem.transform.position.y, Controle.getClicado2().transform.position.x, Controle.getClicado2().transform.position.y, personagem.alcance);

            if (buscaCaminho.busca2())
            {
                if (Controle.getClicado2().setMensagem(personagem))
                {
                    Controle.reiniciaClicados();
                    novoEstado();
                }
            }
        }
    }

    public override void clicado()
    {
        Controle.setClicado(personagem);
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
