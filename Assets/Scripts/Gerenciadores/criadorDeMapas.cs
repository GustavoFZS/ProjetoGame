using UnityEngine;
using System.IO;

public class criadorDeMapas : MonoBehaviour
{
    public string[] mapa;
    int x, y;
    FabricaDePersonages fab;
    FabricaDePisos fab2;

    public void criaMapa(Transform parede, Transform piso, Transform personagem)
    {
        fab = new FabricaDePersonages(personagem);
        fab2 = new FabricaDePisos(parede, piso);

        mapa = File.ReadAllLines("Mapas\\testes2.txt");

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
                if((x + y)%2 == 0)
                {
                    fab2.criaPiso(2, 0, false, x, y);
                }
                else
                {
                    fab2.criaPiso(2, 1, false, x, y);
                }
                fab2.criaPiso(3, 2, false, x, y);
                switch (coluna)
                {
                case '*':
                   fab2.criaPiso(1, 3, true, x, y);
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