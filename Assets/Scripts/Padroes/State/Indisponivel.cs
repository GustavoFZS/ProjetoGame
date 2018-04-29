public class Indisponivel : State
{
    Principal personagem;

    public Indisponivel(Principal personagem) : base(personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        Controle.escolhido = personagem;
    }

    public override void executaAcao()
    {
    }

    public override void novoEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }

}
