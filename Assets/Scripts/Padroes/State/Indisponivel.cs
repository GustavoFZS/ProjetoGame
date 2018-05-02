using UnityEngine;

public class Indisponivel : State
{
    Principal personagem;
    
    public Indisponivel(Principal personagem)
    {
        this.personagem = personagem;
    }

    public void clicado()
    {
        Controle.escolhido = personagem;
    }

    public void executaAcao()
    {    }

    public bool useMouse()
    {
        return false;
    }

    public void novoEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }

    public State clone(Principal personagem)
    {
        return new Indisponivel(personagem);
    }

}
