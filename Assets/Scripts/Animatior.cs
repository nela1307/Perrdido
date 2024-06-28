using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatior : MonoBehaviour
{
    private Animator animator;
    private GameController gameController;

    private void Awake()
    {
        //instanciar el game controller y el animator
        GameObject goController = GameObject.FindGameObjectWithTag("GameController");
        gameController = goController.GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(animator != null)
        {
            animation3Estados();
        }
    }

    private void animation3Estados()
    {
        //pasar la variable del tiempo que queda al animator
        animator.SetFloat("time", gameController.getTime());
    }
}
