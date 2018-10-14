using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClienteModelo : MonoBehaviour
{
    public Socket sckCliente;
    byte[] bytes = new byte[2048];

    public void checkRespostas()
    {
        if (sckCliente.Connected)
        {
            string data = "";
            while (true)
            {
                int bytesRec = sckCliente.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("\n") > -1)
                {
                    break;
                }
            }
            InterpretadorDeMensagens.recebeResposta(data);
        }
    }

     void Awake()
    {
        int porta = Fluxo.porta2;

        IPHostEntry ipHostInfo = Dns.GetHostEntry(Fluxo.ip2);
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, porta);

        sckCliente = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);
    }

    public void EnviaMensagem(string mensagem)
    {
        if (Fluxo.tipoJogo != 2)
        {
            return;
        }

        try
        {
            int porta = Fluxo.porta2;

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Fluxo.ip2);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, porta);
            
            sckCliente = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            sckCliente.Connect(remoteEP);

            Historico.recebeValor("Mensagem enviada: " + mensagem);
            Debug.Log(mensagem);

            byte[] msg = Encoding.UTF8.GetBytes(mensagem + "\n");
            sckCliente.Send(msg, 0, msg.Length, SocketFlags.None);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Conexao.indisponivel = true;
        }
    }
}