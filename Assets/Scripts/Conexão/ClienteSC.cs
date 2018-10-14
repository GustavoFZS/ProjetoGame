using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClienteSC : ClienteModelo
{
    string ip;

    private void Update()
    {
        checkRespostas();
        procuraJogadores();
    }

    public void Start()
    {
        List<string> parametros = new List<string>();
        List<string> valores = new List<string>();

        valores.Add(ServidorP2P.porta.ToString());
        valores.Add(getIpv4());

        parametros.Add("porta");
        parametros.Add("ip");

        string msg = Metodos.criaMensagem("registraJogador", parametros, valores);
        EnviaMensagem(msg);
    }

    public void procuraJogadores()
    {
        string msg = Metodos.criaMensagem("recuperaJogadores");
        EnviaMensagem(msg);
    }

    string getIpv4() {
        string IPAddress = "";
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null;
        Hostname = Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPAddress = Convert.ToString(IP);
            }
        }
        return IPAddress.Replace(":", "teste&");
    }

 }
