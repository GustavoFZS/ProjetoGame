using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{

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
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 18;

        campanha = GUI.Button(new Rect(350, 200, 300, 50), "Modo Campanha", myButtonStyle);
        multiplayer = GUI.Button(new Rect(350, 350, 300, 50), "2 Jogadores (Rede)", myButtonStyle);
        multiplayerLocal = GUI.Button(new Rect(350, 500, 300, 50), "2 Jogadores (Local)", myButtonStyle);
    }
}
