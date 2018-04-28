using UnityEngine;

public class FabricaDePersonages : MonoBehaviour{

    Transform personagem;
    Sprite azul;
    Sprite vermelho;

    public FabricaDePersonages(Transform personagem)
    {
        this.personagem = personagem;
        azul = Resources.Load<Sprite>("Personagem2");
        vermelho = Resources.Load<Sprite>("Personagem");
    }

    public Transform criaPersonagem(int time, int x, int y)
    {
        Transform retorno = Instantiate(personagem, new Vector3(x, y, 0), Quaternion.identity);
        if (time == 1)
        {
            retorno.GetComponent<SpriteRenderer>().sprite = azul;
            return retorno;
        }
        retorno.GetComponent<SpriteRenderer>().sprite = vermelho;
        return retorno;
    }

}
