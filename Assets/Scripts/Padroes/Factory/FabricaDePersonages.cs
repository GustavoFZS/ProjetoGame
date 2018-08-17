using UnityEngine;

public class FabricaDePersonages : MonoBehaviour{

    Transform personagem;
    Sprite mago, barbaro, arqueiro, troll, imagem;
    int tipo = 0;

    public FabricaDePersonages(Transform personagem)
    {
        this.personagem = personagem;
        mago = Resources.Load<Sprite>("Mago");
        barbaro = Resources.Load<Sprite>("Barbaro");
        arqueiro = Resources.Load<Sprite>("Arqueiro");
        troll = Resources.Load<Sprite>("Troll");

    }

    public Transform criaPersonagem(int time, float x, float y)
    {
        Transform retorno = Instantiate(personagem, new Vector3(x, y, 0), Quaternion.identity);
        Principal info = retorno.GetComponent<Principal>();
        info.time = time;

        switch (tipo)
        {
            case 0:
                imagem = mago;
                info.ataque = 10;
                break;
            case 1:
                imagem = barbaro;
                info.ataque = 15;
                break;
            case 2:
                imagem = arqueiro;
                info.ataque = 20;
                break;
            case 3:
                imagem = troll;
                info.ataque = 25;
                break;
        }

        tipo = ((tipo+1)%4);
        retorno.GetComponent<SpriteRenderer>().sprite = imagem;
        return retorno;
    }

}
