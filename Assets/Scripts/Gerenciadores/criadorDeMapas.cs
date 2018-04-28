using UnityEngine;
using System.IO;

public class criadorDeMapas : MonoBehaviour
{
    public string[] mapa;
    int x, y;
    FabricaDePersonages fab;

    public void criaMapa(Transform parede, Transform personagem)
    {
        fab = new FabricaDePersonages(personagem);

        mapa = File.ReadAllLines("Mapas\\testes.txt");

        float cameraAltura = 2f * Camera.main.orthographicSize;
        float cameraLargura = cameraAltura * Camera.main.aspect;

        y = Mathf.RoundToInt(cameraAltura/2);
        foreach (string linha in mapa)
        {
            y--;
            x = -Mathf.RoundToInt(cameraLargura/2);
            foreach (char coluna in linha)
            {
                x++;
                if(coluna == '*')
                    Instantiate(parede, new Vector3(x, y, 0), Quaternion.identity);
                if (coluna == 'A')
                    MontaTimeA(x, y);
                if (coluna == 'B')
                    MontaTimeB(x, y);
            }
        }
    }

    void MontaTimeA(int x, int y)
    {
        fab.criaPersonagem(0, x, y);
    }

    void MontaTimeB(int x, int y)
    {
        fab.criaPersonagem(1, x, y);
    }
}