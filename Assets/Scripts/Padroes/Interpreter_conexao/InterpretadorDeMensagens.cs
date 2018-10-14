using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class InterpretadorDeMensagens : MonoBehaviour
{
    public static void recebeResposta(string msg)
    {
        string[] decode = msg.Split(';');
        string status = decode[0].Split(':')[1];
        string metodo = decode[1].Split(':')[1];
        Dictionary< string, string> parametros = new Dictionary<string, string>();

        Debug.Log(metodo);

        if (msg.Contains("parametros"))
        {
            string variaveis = decode[2].Split(':')[1];
            string[] valores = variaveis.Split(',');
            foreach(string valore in valores)
            {
                string nome = valore.Split('=')[0];
                string valor = valore.Split('=')[1];
                parametros.Add(nome, valor);
            }
        }
        
        if (metodo.Equals("mudaTurno"))
        {
            Controle.mudaTurno();
        }

        if (metodo.Contains("registraJogador"))
        {
            string time;
            parametros.TryGetValue("time", out time);
            Controle.time = int.Parse(time);
        }

        if (metodo.Contains("recuperaJogadores"))
        {          
            if (!status.Equals("200"))
            {
                Debug.Log("Erro: " + status);
                return;
            }

            string porta1, porta2, ip1, ip2;
            parametros.TryGetValue("porta1", out porta1);
            parametros.TryGetValue("porta2", out porta2);
            parametros.TryGetValue("ip1", out ip1);
            parametros.TryGetValue("ip2", out ip2);

            if (Controle.time == 1)
            {
                Fluxo.porta2 = Convert.ToInt32(porta2);
                Fluxo.ip2 = ip2.Replace("teste&", ":");
            }
            else
            {
                Fluxo.porta2 = Convert.ToInt32(porta1);
                Fluxo.ip2 = ip1.Replace("teste&", ":");
            }

            SceneManager.LoadScene("2 Play (Rede)", LoadSceneMode.Single);
        }

    }

    public static string recebeMensagem(string msg)
    {
        Debug.Log(msg);

        string[] decode = msg.Split(';');
        string metodo = decode[0].Split(':')[1];
        string retorno = "";
        Dictionary<string, string> parametros = new Dictionary<string, string>();

        if (msg.Contains("parametros"))
        {
            string variaveis = decode[1].Split(':')[1];
            string[] valores = variaveis.Split(',');
            foreach (string valore in valores)
            {
                string nome = valore.Split('=')[0];
                string valor = valore.Split('=')[1];
                parametros.Add(nome, valor);
            }
        }

        if (metodo.Equals("mudaTurno"))
        {
            Controle.mudaTurno();
            retorno = Metodos.criaMensagem(metodo, "status", "200");
        }

        if (metodo.Equals("movimenta"))
        {
            LinkedList<GameObject> players = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            LinkedList<GameObject>.Enumerator enu = players.GetEnumerator();
            while (enu.MoveNext())
            {
                Personagem per = enu.Current.GetComponent<Personagem>();
                string id;
                parametros.TryGetValue("id", out id);
                if (per.id.Equals(id))
                {
                    string pos_x;
                    string pos_y;
                    parametros.TryGetValue("pos_x", out pos_x);
                    parametros.TryGetValue("pos_y", out pos_y);
                    Vector2 vec = new Vector2(float.Parse(pos_x), float.Parse(pos_y));
                    if (per.anda(vec, true))
                    {
                        retorno = Metodos.criaMensagem(metodo, "status", "200");
                    }
                    else
                    {
                        retorno = Metodos.criaMensagem(metodo, "status", "300");
                    }
                    break;
                }
            }
        }

        if (metodo.Equals("habilidade"))
        {
            Personagem origem = new Personagem();
            Personagem destino = new Personagem();

            LinkedList<GameObject> players = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            LinkedList<GameObject>.Enumerator enu = players.GetEnumerator();
            while (enu.MoveNext())
            {
                Personagem per = enu.Current.GetComponent<Personagem>();
                string id_destino;
                string id_origem;
                parametros.TryGetValue("id_destino", out id_destino);
                parametros.TryGetValue("id_origem", out id_origem);
                if (per.id.Equals(id_destino))
                {
                    destino = per;
                }
                if (per.id.Equals(id_origem))
                {
                    origem = per;
                }
            }
            string habilidade;
            parametros.TryGetValue("habilidade", out habilidade);
            Interpretador interpretador = new InterpretadorDeHabilidades();
            if (interpretador.recebeMensagem(destino, origem, habilidade, false))
            {
                retorno = Metodos.criaMensagem(metodo, "status", "200");
            }
            else
            {
                retorno = Metodos.criaMensagem(metodo, "status", "300");
            }
        }

        return retorno;
    }
}