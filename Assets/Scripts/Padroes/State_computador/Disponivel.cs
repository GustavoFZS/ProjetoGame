public class Disponivel : State
{
    Personagem personagem;

    public Disponivel(Personagem personagem)
    {
        this.personagem = personagem;
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Escolhido(personagem));
    }

}
