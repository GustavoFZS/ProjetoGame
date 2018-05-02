using UnityEngine;

public class PodeAndaEAtacar : State
{
    Principal personagem;

    public PodeAndaEAtacar(Principal personagem)
    {
        this.personagem = personagem;
    }

    public void clicado()
    {
        Debug.Log("achei");
        novoEstado();
        Controle.escolhido = personagem;
    }

    public void executaAcao()
    {
    }


    public bool useMouse()
    {
        return false;
    }

    public void novoEstado()
    {
        personagem.setEstado(new Selecionado(personagem));
    }

    public State clone(Principal personagem)
    {
        return new PodeAndaEAtacar(personagem);
    }

}
