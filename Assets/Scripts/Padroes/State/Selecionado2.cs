public class Selecionado2 : State
{
    Principal personagem;

    public Selecionado2(Principal personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        Controle.escolhido = personagem;
        personagem.mudaBox();
    }

    public void clicado()
    {
    }

    public void executaAcao()
    {
        if (!Controle.checaAcao(personagem.time))
        {
            Controle.escolhido.setMensagem(personagem.getHabilidade());
            novoEstado();
        }
    }

    public bool useMouse()
    {
        return true;
    }

    public void novoEstado()
    {
        personagem.setEstado(new Indisponivel(personagem));
    }

    public State clone(Principal personagem)
    {
        return new Selecionado2(personagem);
    }

}
