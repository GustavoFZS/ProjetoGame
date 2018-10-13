using System;
using UnityEngine;

public abstract class Interpretador : MonoBehaviour
{
    public abstract bool recebeMensagem(Personagem destino, Personagem emissor, string habilidade, bool local = true);

    public virtual Interpretador clone()
    {
        return this;
    }
}
