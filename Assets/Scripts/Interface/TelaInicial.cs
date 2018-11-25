using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{

    public static string erro = "";
    public static string msg = "";

    float cameraAltura;
    float cameraLargura;
    bool campanha;
    bool multiplayer;
    bool multiplayerLocal;

    // Use this for initialization
    void Start()
    {
        cameraAltura = 2f * Camera.main.orthographicSize;
        cameraLargura = cameraAltura * Camera.main.aspect;

        campanha = false;
        multiplayer = false;
        multiplayerLocal = false;
    }

    // Use this for initialization
    void Update()
    {
        if (campanha)
        {
            SceneManager.LoadScene("Campanha", LoadSceneMode.Single);
            Fluxo.tipoJogo = 1;
        }

        if (multiplayer)
        {
            SceneManager.LoadScene("Tela de espera", LoadSceneMode.Single);
            Fluxo.tipoJogo = 2;
        }

        if (multiplayerLocal)
        {
            SceneManager.LoadScene("2 Play", LoadSceneMode.Single);
            Fluxo.tipoJogo = 3;
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
        myLabelStyle.fontSize = 25;
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(100, 100, 300, 50), "O jogo sem nome", myLabelStyle);
        myLabelStyle.fontSize = 12;
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(100, 135, 300, 50), "PSG II - 2018", myLabelStyle);

        if (!msg.Equals(""))
        {
            myLabelStyle.fontSize = 25;
            GUI.contentColor = Color.blue;
            GUI.Label(new Rect(100, 250, 800, 150), msg, myLabelStyle);
        }

        if (!erro.Equals(""))
        {
            myLabelStyle.fontSize = 25;
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(100, 300, 800, 150), erro, myLabelStyle);
        }

        GUI.contentColor = Color.black;
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 18;

        GUI.Button(new Rect(40, 600, 300, 50), "Modo Campanha", myButtonStyle);
        multiplayer = GUI.Button(new Rect(350, 600, 300, 50), "2 Jogadores (Rede)", myButtonStyle);
        multiplayerLocal = GUI.Button(new Rect(660, 600, 300, 50), "2 Jogadores (Local)", myButtonStyle);
    }
}
