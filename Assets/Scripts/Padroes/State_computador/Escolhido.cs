public class Escolhido : State
{
    Personagem personagem;

    public Escolhido(Personagem personagem)
    {

    }

    public override void executaAcao()
    {

    }

    public override bool useHab(string novaHab)
    {
        personagem.setHabilidade(novaHab);
        return true;
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Escolhido(personagem));
    }

    void finaliza()
    {
        personagem.setEstado(new Indisponivel2(personagem));
    }

}
