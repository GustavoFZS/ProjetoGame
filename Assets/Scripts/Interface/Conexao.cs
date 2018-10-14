using UnityEngine;
using UnityEngine.SceneManagement;

public class Conexao : MonoBehaviour
{
    public static bool indisponivel = false;
    ClienteSC sc;

    bool voltaBotao;
    bool reiniciarBotao;
    string texto = "Aguardando conexão...";

    private void Start()
    {
        sc = new ClienteSC();
    }

    void Update()
    {
        if (voltaBotao)
        {
            SceneManager.LoadScene("Tela inicial", LoadSceneMode.Single);
        }
    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(400, 350, 300, 50), texto);

        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 18;

        voltaBotao = GUI.Button(new Rect(350, 400, 300, 50), "Voltar", myButtonStyle);

        if (indisponivel)
        {
            texto = "Servidor indisponível";
        }

        if (Fluxo.porta2 == 0 && !indisponivel)
        {
            texto = "Aguarde outro jogador...";
        }

    }
}
