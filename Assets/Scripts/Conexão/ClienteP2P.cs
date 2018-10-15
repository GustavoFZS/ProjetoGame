using System.Collections.Generic;

public class ClienteP2P : ClienteModelo
{
    private void Update()
    {
        checkRespostas();
    }

    public void EnviaHabMsg(string receptor_id, string origem_id, string hab)
    {
        List<string> parametros = new List<string>();
        List<string> valores = new List<string>();

        valores.Add(receptor_id);
        valores.Add(origem_id);
        valores.Add(hab);

        parametros.Add("id_destino");
        parametros.Add("id_origem");
        parametros.Add("habilidade");

        string msg = Metodos.criaMensagem("habilidade", parametros, valores);

        EnviaMensagem(msg);
    }

    public void EnviaMovMsg(string id, float x, float y)
    {
        List<string> parametros = new List<string>();
        List<string> valores = new List<string>();

        valores.Add(id);
        valores.Add(x.ToString());
        valores.Add(y.ToString());

        parametros.Add("id");
        parametros.Add("pos_x");
        parametros.Add("pos_y");

        string msg = Metodos.criaMensagem("movimenta", parametros, valores);

        EnviaMensagem(msg);
    }

    public void EnviaTurnMsg()
    {
        string msg = Metodos.criaMensagem("mudaTurno");
        EnviaMensagem(msg);
    }
}