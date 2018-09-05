using System;
using UnityEngine;

public abstract class Interpretador : MonoBehaviour
{

    public abstract bool recebeMensagem(Personagem destino, Personagem emissor, Transform prefab);

    public virtual Interpretador clone()
    {
        return this;
    }

}
