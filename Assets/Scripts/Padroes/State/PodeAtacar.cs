public class PodeAtacar : State
{
    Principal personagem;

    public PodeAtacar(Principal personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        novoEstado();
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Selecionado2(personagem));
    }

}
