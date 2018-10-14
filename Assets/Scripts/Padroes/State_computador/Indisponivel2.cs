public class Indisponivel2 : State
{
    Personagem personagem;
    
    public Indisponivel2(Personagem personagem)
    {
        this.personagem = personagem;
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Disponivel(personagem));
    }

}