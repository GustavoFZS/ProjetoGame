using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServidorP2P : MonoBehaviour
{
    public static int porta = 11005;

    string data;
    byte[] bytes = new Byte[2048];

    IPHostEntry ipHostInfo;
    IPAddress ipAddress;
    IPEndPoint localEndPoint;
    Socket listener;

    void Update()
    {
        if (listener.Poll(1000, SelectMode.SelectRead))
        {
            Socket handler = listener.Accept();

            if (handler.Available > 0)
            {
                data = "";
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("\n") > -1)
                    {
                        break;
                    }
                }

                string resposta = InterpretadorDeMensagens.recebeMensagem(data);
                byte[] msg = Encoding.ASCII.GetBytes(resposta);

                Historico.recebeValor("Mensagem recebida: " + data);
                Historico.recebeValor("Mensagem enviada: " + resposta);

                handler.Send(msg, 0, msg.Length, SocketFlags.None);
                handler.Shutdown(SocketShutdown.Send);
                handler.Close();
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

            listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(10);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}