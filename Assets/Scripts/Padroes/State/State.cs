using UnityEngine;

public interface State
{
    void clicado();

    void executaAcao();

    void novoEstado();

    bool useMouse();

    State clone(Principal personagem);

}
