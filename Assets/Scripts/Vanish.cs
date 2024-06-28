using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    [SerializeField]
    private GameObject stars;
    [SerializeField] 
    private float destroyTime;
    [SerializeField]
    private float extraTime;
    [SerializeField]
    private int extraPoints;
    [SerializeField]
    private GameObject animacion;

    private GameController gameController;
    public void vanishing()
    {
        //instancia la animacion de coger al perro
        GameObject animation = Instantiate(stars, transform.position, transform.rotation);
        animation.transform.parent = transform.parent;

        //calcula los puntos y el tiempo extra que ganas
        gameController.calcPoints(extraPoints,extraTime);
        Destroy(gameObject);
    }

    private void Awake()
    {
        //instanciar el game controller e invocar el metodo de destruir por tiempo
        GameObject goController = GameObject.FindGameObjectWithTag("GameController");
        gameController = goController.GetComponent<GameController>();
        Invoke(nameof(destroyWithTime),destroyTime);
    }

    private void destroyWithTime()
    {
        //instanciar la animacion al desaparecer
        GameObject a = Instantiate(animacion, transform.position, transform.rotation);

        a.transform.parent = transform.parent;
        //resta 3 segundos si el perro desaparece
        gameController.calcPoints(0,3);
        
        Destroy(gameObject);
    }



}
