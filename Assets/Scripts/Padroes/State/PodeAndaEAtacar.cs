public class PodeAndaEAtacar : State
{
    Principal personagem;

    public PodeAndaEAtacar(Principal personagem) : base(personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        novoEstado();
        Controle.escolhido = personagem;
    }

    public override void executaAcao()
    {
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Selecionado(personagem));
    }

}
