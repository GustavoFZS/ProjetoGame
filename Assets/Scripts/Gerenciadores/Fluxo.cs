using System.IO;
using UnityEngine;

public class Fluxo : MonoBehaviour
{
    public static int tipoJogo = 0;
    public static int porta2 = 11522;
    public static string ip2 = "";
    public static bool partidaInicada = false;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;

            string[] endereco = File.ReadAllLines("Config\\server_ip.txt");

            ip2 = endereco[0];
        }
    }
}
