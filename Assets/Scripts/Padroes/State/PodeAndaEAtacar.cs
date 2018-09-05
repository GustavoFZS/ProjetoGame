using UnityEngine;

public class PodeAndaEAtacar : State
{
    Personagem personagem;

    public PodeAndaEAtacar(Personagem personagem)
    {
        this.personagem = personagem;
    }

    public override void clicado()
    {
        Controle.setClicado(personagem);
        novoEstado();
    }

    public override bool useMouse()
    {
        return false;
    }

    public override void novoEstado()
    {
        personagem.apagaRotas();
        personagem.setEstado(new Selecionado(personagem));
    }

}
