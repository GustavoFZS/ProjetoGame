public class PodeAtacar : State
{
    Principal personagem;

    public PodeAtacar(Principal personagem)
    {
        this.personagem = personagem;
    }

    public void clicado()
    {
        novoEstado();
    }

    public void executaAcao()
    {}

    public bool useMouse()
    {
        return false;
    }

    public void novoEstado()
    {
        personagem.setEstado(new Selecionado2(personagem));
    }

    public State clone(Principal personagem)
    {
        return new PodeAtacar(personagem);
    }

}
