public class PodeAtacar : State
{
    Principal personagem;

    public PodeAtacar(Principal personagem) : base(personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        novoEstado();
    }

    public override void executaAcao()
    {

    }

    public override void novoEstado()
    {
        personagem.setEstado(new Selecionado2(personagem));
    }

}
