using UnityEngine;

public class Selecionado : State
{
    Principal personagem;

    public Selecionado(Principal personagem)
    {
        this.personagem = personagem;
        personagem.mudaBox();
        Controle.escolhido = personagem;
        personagem.GetComponent<Movimentacao>().mostraRota();
        personagem.mudaBox();
        Debug.Log("masq?");
    }

    public void clicado()
    {
    }

    public void executaAcao()
    {
        Debug.Log("masq2?");
        if (Controle.checaAcao(personagem.time)){
            if (personagem.anda())
            {
                novoEstado();
                Debug.Log("masq3?");
            }
            else
            {
                voltaEstado();
                personagem.apagaRotas();
                Debug.Log("masq4?");
            }
        }
        else
        {
            Controle.escolhido.setMensagem(personagem.getHabilidade());
            finaliza();
            personagem.apagaRotas();
            Debug.Log("masq5?");
        }
    }

    public bool useMouse()
    {
        return true;
    }

    public void novoEstado()
    {
        personagem.setEstado(new PodeAtacar(personagem));
    }

    void finaliza()
    {
      //  personagem.setEstado(new Indisponivel(personagem));
    }

    void voltaEstado()
    {
        personagem.setEstado(new PodeAndaEAtacar(personagem));
    }

    public State clone(Principal personagem)
    {
        return new Selecionado(personagem);
    }
}
