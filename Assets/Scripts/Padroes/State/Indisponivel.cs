using UnityEngine;

public class Indisponivel : State
{
    Principal personagem;
    
    public Indisponivel(Principal personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        Controle.escolhido = personagem;
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }

}
