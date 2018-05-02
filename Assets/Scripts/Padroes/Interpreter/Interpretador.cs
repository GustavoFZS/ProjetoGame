public abstract class Interpretador
{

    public abstract void recebeMensagem(Principal destino, string mensagem);

    public virtual Interpretador clone()
    {
        return this;
    }

}
