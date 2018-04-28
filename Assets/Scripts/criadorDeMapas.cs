using UnityEngine;
using UnityEditor;
using System.IO;

public class criadorDeMapas : MonoBehaviour
{
    public string[] mapa;
    int x, y;
    Transform prefab;

    public void criaMapa(Transform prefab)
    {
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
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

}