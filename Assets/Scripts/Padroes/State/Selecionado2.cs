public class Selecionado2 : State
{
    Principal personagem;

    public Selecionado2(Principal personagem) : base(personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        Controle.escolhido = personagem;
        personagem.mudaBox();
    }

    public override void clicado()
    {
    }

    public override void executaAcao()
    {
        if (!Controle.checaAcao(personagem.time))
        {
            Controle.escolhido.setMensagem(personagem.getHabilidade());
            novoEstado();
        }
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Indisponivel(personagem));
    }

}
