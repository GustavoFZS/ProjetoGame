using UnityEngine;

public class Indisponivel : State
{
    Personagem personagem;
    
    public Indisponivel(Personagem personagem)
    {
        this.personagem = personagem;
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
