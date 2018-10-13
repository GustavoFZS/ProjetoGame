using System.Collections.Generic;
using UnityEngine;

public class FabricaDePersonages : MonoBehaviour{

    Transform personagem;
    Dictionary<int, MetaDataChars> metaDatas = new Dictionary<int, MetaDataChars>();
    int tipo = 0;

    string padrao = "Padrao%tipo-0";
    string cura = "Cura%tipo:1|vida:10|vida_duracao-3";
    string furia = "Fúria%tipo:0|ataque-50";
    string corre = "Corre%tipo:3|movimentacao-10";

    public FabricaDePersonages(Transform personagem)
    {
        int indice = 0;
        metaDatas.Add(indice++, new MetaDataChars(100, 150, 70, 4, 4, Resources.Load<Sprite>("Mago"), "Mago", padrao, cura, corre));
        metaDatas.Add(indice++, new MetaDataChars(190, 160, 100, 5, 3, Resources.Load<Sprite>("Barbaro"), "Barbaro", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(80, 130, 70, 4, 5, Resources.Load<Sprite>("Arqueiro"), "Arqueiro", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(210, 170, 85, 3, 3, Resources.Load<Sprite>("Troll"), "Troll", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(110, 200, 75, 3, 4, Resources.Load<Sprite>("Mago2"), "Mago2", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(200, 100, 150, 2, 3, Resources.Load<Sprite>("Regenerator"), "Regenerator", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(250, 120, 200, 2, 2, Resources.Load<Sprite>("Rocha"), "Rocha", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(60, 210, 60, 2, 4, Resources.Load<Sprite>("Samurai"), "Samurai", padrao, cura, furia));
        metaDatas.Add(indice++, new MetaDataChars(60, 250, 50, 2, 6, Resources.Load<Sprite>("Morcego"), "Morcego", padrao, corre, furia));

        this.personagem = personagem;

    }

    public Transform criaPersonagem(int time, float x, float y)
    {
        Transform retorno = Instantiate(personagem, new Vector3(x, y, 0), Quaternion.identity);
        Personagem info = retorno.GetComponent<Personagem>();

        tipo = ((tipo + 1) % 9);

        info.setaAtributos(metaDatas[tipo].vida, metaDatas[tipo].ataque, metaDatas[tipo].alcance, metaDatas[tipo].movimentacao, metaDatas[tipo].defesa, time, metaDatas[tipo].nome, metaDatas[tipo].habPadrao, metaDatas[tipo].hab1, metaDatas[tipo].hab2);

        retorno.GetComponent<SpriteRenderer>().sprite = metaDatas[tipo].sprite;
        return retorno;
    }

}
