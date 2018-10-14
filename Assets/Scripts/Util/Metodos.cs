using UnityEngine;
using System.Collections.Generic;

public class Metodos : MonoBehaviour
{
    static string marcador = "\n";

    public static Vector3 getPosicaoMouseNaGrid()
    {

        Vector2 vetorAux;

        vetorAux = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        vetorAux = new Vector2(Mathf.Round(vetorAux.x / Controle.tamanhoCasas) * Controle.tamanhoCasas, Mathf.Round(vetorAux.y / Controle.tamanhoCasas) * Controle.tamanhoCasas);

        return vetorAux;

    }

    public static LinkedList<Personagem> intersecçãoDeLista(LinkedList<Personagem> l1, LinkedList<Personagem> l2)
    {
        LinkedList<Personagem>.Enumerator enu = l1.GetEnumerator();
        LinkedList<Personagem> resultado = new LinkedList<Personagem>();

        while (enu.MoveNext())
        {
            if (l2.Contains(enu.Current))
            {
                resultado.AddLast(enu.Current);
            }
        }

        return resultado;
    }

    public static string criaMensagem(string nome)
    {
        string retorno = "metodo:" + nome + ";";
        return retorno;
    }

    public static string criaMensagem(string nome, string parametro, string valor)
    {
        string retorno = "metodo:" + nome + ";";
        retorno += "parametros:";
        retorno += parametro + "=" + valor;
        retorno += ";";
        return retorno;
    }

    public static string criaMensagem(string nome, List<string> parametros, List<string> valores)
    {
        string retorno = "metodo:" + nome + ";";
        retorno += "parametros:";
        for(int i = 0; i < (parametros.Count - 1); i++)
        {
            retorno += parametros[i] + "=" + valores[i] + ",";
        }
        retorno += parametros[parametros.Count - 1] + "=" + valores[parametros.Count - 1];
        retorno += ";";
        return retorno;
    }

    public static bool checaAcao()
    {
        return Fluxo.tipoJogo != 2 || (Controle.time == Controle.turno);
    }

}
