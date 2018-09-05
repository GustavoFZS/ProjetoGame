using UnityEngine;

public class CameraControlador : MonoBehaviour {

    int velocidade = 25;
    float cameraAltura;
    float cameraLargura;

    // Use this for initialization
    void Start () {

        cameraAltura = Controle.cameraAltura;
        cameraLargura = Controle.cameraLargura;
        transform.position = new Vector3(1f,-1f,-10f);

    }
	
	// Update is called once per frame
	void Update () {
        
        float cameraPosX = transform.position.x + cameraLargura;
        float cameraPosY = ((-transform.position.y) + cameraAltura);

        if (Input.GetKey(KeyCode.RightArrow) && cameraPosX < Controle.getLarguraMapa())
        {
            transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > 1)
        {
            transform.Translate(new Vector3(-velocidade * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) && cameraPosY < Controle.getAlturaMapa())
        {
            transform.Translate(new Vector3(0, -velocidade * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < -1)
        {
            transform.Translate(new Vector3(0, velocidade * Time.deltaTime, 0));
        }
    }
}
