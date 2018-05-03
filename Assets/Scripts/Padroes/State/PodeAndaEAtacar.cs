using UnityEngine;

public class PodeAndaEAtacar : State
{
    Principal personagem;

    public PodeAndaEAtacar(Principal personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        novoEstado();
        Controle.escolhido = personagem;
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new Selecionado(personagem));
    }

}
