using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServidorP2P : MonoBehaviour
{
    public static int porta = 11553;

    byte[] bytes = new Byte[2048];

    IPHostEntry ipHostInfo;
    IPAddress ipAddress;
    IPEndPoint localEndPoint;
    Socket sckServidor;

    void Update()
    {
        if (sckServidor.Poll(100, SelectMode.SelectRead))
        {
            Socket sckNovo = sckServidor.Accept();

            if (sckNovo.Available > 0)
            {
                string mensagem = "";
                while (true)
                {
                    int dadosRecebidos = sckNovo.Receive(bytes);
                    mensagem += Encoding.ASCII.GetString(bytes, 0, dadosRecebidos);
                    if (mensagem.IndexOf("\n") > -1)
                    {
                        break;
                    }
                }

                Historico.recebeValor("Mensagem recebida: " + mensagem);

                if (mensagem.Contains("status"))
                {
                    InterpretadorDeMensagens.recebeResposta(mensagem);
                }
                else
                {
                    string resposta = InterpretadorDeMensagens.recebeMensagem(mensagem);
                    byte[] msg = Encoding.ASCII.GetBytes(resposta);
                    Controle.cliente.EnviaMensagem(resposta);
                }

                sckNovo.Shutdown(SocketShutdown.Both);
                sckNovo.Close();
            }
        }
    }

    void Start()
    {
        try
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, porta);

            sckServidor = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            sckServidor.Bind(localEndPoint);
            sckServidor.Listen(10);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}