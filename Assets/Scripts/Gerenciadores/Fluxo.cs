using System.IO;
using UnityEngine;

public class Fluxo : MonoBehaviour
{
    public static int tipoJogo = 0;
    public static int porta2 = 11000;
    public static string ip2 = "";

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;

            string[] endereco = File.ReadAllLines("Config\\server_ip.txt");

            foreach (string linha in endereco)
            {
                ip2 = linha;
            }

        }
    }
}
