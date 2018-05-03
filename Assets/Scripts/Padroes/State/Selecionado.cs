public class Selecionado : State
{
    Principal personagem;

    public Selecionado(Principal personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        Controle.escolhido = personagem;
        personagem.GetComponent<Movimentacao>().mostraRota();
        personagem.mudaBox();
    }

    public override void executaAcao()
    {
        if (Controle.checaAcao(personagem.time)){
            if (personagem.anda())
            {
                novoEstado();
            }
            else
            {
                voltaEstado();
                personagem.apagaRotas();
            }
        }
        else
        {
            Controle.escolhido.setMensagem(personagem.getHabilidade());
            finaliza();
            personagem.apagaRotas();
        }
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
      //  personagem.setEstado(new Indisponivel(personagem));
    }

    void voltaEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }
}
