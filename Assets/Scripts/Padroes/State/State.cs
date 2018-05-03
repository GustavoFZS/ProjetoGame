using UnityEngine;

public abstract class State
{
    public virtual void clicado() {
    }

    public virtual void executaAcao() {
    }

    public abstract void novoEstado();

    public abstract bool useMouse();
    
}
