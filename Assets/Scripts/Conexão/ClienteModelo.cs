using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClienteModelo : MonoBehaviour
{
    public Socket sckCliente;
    byte[] bytes = new byte[2048];

    public void checkRespostas()
    {
        try
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
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Conexao.indisponivel = true;
            SceneManager.LoadScene("Tela inicial", LoadSceneMode.Single);
            TelaInicial.erro = "Houve algum problmea com o servidor, por favor tente novamente";
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

            Debug.Log(Fluxo.ip2 + ":" + porta);
            Debug.Log(mensagem);

            sckCliente = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            sckCliente.Connect(remoteEP);

            Historico.recebeValor("Mensagem enviada: " + mensagem);
            Controle.ultimaMsg = mensagem;

            byte[] msg = Encoding.UTF8.GetBytes(mensagem + "\n");
            sckCliente.Send(msg, 0, msg.Length, SocketFlags.None);

            Conexao.indisponivel = false;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Conexao.indisponivel = true;
            SceneManager.LoadScene("Tela inicial", LoadSceneMode.Single);
            TelaInicial.erro = "Ops! Houve algum problema com a conexão";
        }
    }
}