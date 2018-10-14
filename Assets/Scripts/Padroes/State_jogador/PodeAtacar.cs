public class PodeAtacar : State
{
    Personagem personagem;

    public PodeAtacar(Personagem personagem)
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
        personagem.apagaRotas();
        personagem.setEstado(new Selecionado2(personagem));
    }

}
