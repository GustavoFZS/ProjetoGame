using UnityEngine;

public class Historico : MonoBehaviour
{

    public static string[] valores = new string[] { "Inicio da partida.", "", "", "", "", "", "", "", "" };

    void OnGUI()
    {
        for (int i = 0; i < valores.Length; i++)
        {
            GUI.Label(new Rect(10, 650 - (30 * i), 590, 50), valores[i].ToString());
        }
    }

    public static void recebeValor(string mensagem)
    {
        for (int i = 0; i < valores.Length - 1; i++)
        {
            valores[valores.Length - 1 - i] = valores[valores.Length - 2 - i];
        }
        valores[0] = mensagem;
    }

}