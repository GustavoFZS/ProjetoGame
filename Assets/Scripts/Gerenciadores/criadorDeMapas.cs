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

        float cameraAltura = Controle.cameraAltura;
        float cameraLargura = Controle.cameraLargura;

        y = Mathf.FloorToInt(cameraAltura/2);
        foreach (string linha in mapa)
        {
            y-= Controle.tamanhoCasas;
            x = -Mathf.FloorToInt(cameraLargura/2) - Mathf.FloorToInt(Controle.tamanhoCasas/2);
            foreach (char coluna in linha)
            {
                x+= Controle.tamanhoCasas;
                switch (coluna)
                {
                case '*':
                    parede = Instantiate(parede, new Vector3(x, y, 0), Quaternion.identity);
                    parede.transform.localScale = new Vector3(Controle.tamanhoCasas, Controle.tamanhoCasas);
                    break;
                case 'A':
                    MontaTimeA(x, y);
                    break;
                case 'B':
                    MontaTimeB(x, y);
                    break;
                }
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