using System.Collections.Generic;
using UnityEngine;

public class FabricaDePersonages : MonoBehaviour{

    Transform personagem;
    Dictionary<int, MetaDataChars> metaDatas = new Dictionary<int, MetaDataChars>();
    Sprite mago, barbaro, arqueiro, troll, imagem, mago2, regenerator, rocha, samurai, morcego;
    int tipo = 0;

    string cura = "Cura|0|2|";
    string furia = "Fúria|1|-1|";
    string corre = "Corre|2|0|";

    public FabricaDePersonages(Transform personagem)
    {
        int indice = 0;
        metaDatas.Add(indice++, new MetaDataChars(100, 150, 70, 4, 4, Resources.Load<Sprite>("Mago"), "Mago", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(190, 160, 100, 5, 2, Resources.Load<Sprite>("Barbaro"), "Barbaro", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(80, 130, 70, 4, 5, Resources.Load<Sprite>("Arqueiro"), "Arqueiro", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(210, 170, 85, 3, 2, Resources.Load<Sprite>("Troll"), "Troll", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(110, 200, 75, 3, 4, Resources.Load<Sprite>("Mago2"), "Mago2", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(200, 100, 150, 2, 3, Resources.Load<Sprite>("Regenerator"), "Regenerator", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(250, 120, 200, 2, 2, Resources.Load<Sprite>("Rocha"), "Rocha", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(60, 210, 60, 2, 4, Resources.Load<Sprite>("Samurai"), "Samurai", cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(60, 250, 50, 2, 6, Resources.Load<Sprite>("Morcego"), "Morcego", corre, furia));

        this.personagem = personagem;

    }

    public Transform criaPersonagem(int time, float x, float y)
    {
        Transform retorno = Instantiate(personagem, new Vector3(x, y, 0), Quaternion.identity);
        Personagem info = retorno.GetComponent<Personagem>();

        tipo = ((tipo + 1) % 9);

        info.setaAtributos(metaDatas[tipo].vida, metaDatas[tipo].ataque, metaDatas[tipo].alcance, metaDatas[tipo].movimentacao, metaDatas[tipo].defesa, time, metaDatas[tipo].nome, metaDatas[tipo].hab1, metaDatas[tipo].hab2);

        retorno.GetComponent<SpriteRenderer>().sprite = metaDatas[tipo].sprite;
        return retorno;
    }

}
