using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI pointsUI;
    [SerializeField]
    private TextMeshProUGUI timeUI;
    [SerializeField]
    private TextMeshProUGUI gameOverUI;
    [SerializeField]
    private TextMeshProUGUI resetUI;

    [Header("Control")]
    [SerializeField]
    private float startTime;
    [SerializeField]
    //tiempo de espera antes de empezar los spawns
    private float waitTime;
    [SerializeField]
    //tiempo de espera entre el spawn de cada perro
    private float waitDogs;
    [SerializeField]
    //tiempo de espera entre la desaparicion y la reaparicion de los perros
    private float waveTime;
    [SerializeField]
    GameObject spawn;
    [SerializeField]
    GameObject[] dogs;

    
    GameObject[] spawns;
    BoxCollider[] spawnCollider;

    private int totalPoints;
    private float remainingTime;
    private float extraTime;
    private float endTime;
    private bool gameOver;



    private void Awake()
    {

        int childCount = spawn.transform.childCount;
        spawns = new GameObject[childCount];
        spawnCollider = new BoxCollider[childCount];

        //recoger los spawns del gameObject spawn y sus colliders
        for (int i = 0; i < childCount; i++)
        {
            spawns[i] = spawn.transform.GetChild(i).gameObject;
            spawnCollider[i] = spawns[i].GetComponent<BoxCollider>();
           
        }

        totalPoints = 0;
        extraTime = 0;
        gameOver = false;

        endTime = Time.time + startTime;
        calcTime();

    }

    public void calcTime()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Tiempo restante: ");
        remainingTime = endTime - Time.time + extraTime;
        sb.Append(remainingTime.ToString("0.0"));

        //añadir tiempo UI
        timeUI.text = sb.ToString();
    }

    private IEnumerator spawnDogs()
    {
        yield return new WaitForSeconds(waitTime);

        while(true)
        {
            //aparecen 3 perros cada vez
            for (int i = 0;i < 3; i++)
            {

                //calcular spawn aleatorio
                int random = Random.Range(0,spawns.Length);
                Vector3 center = getCenter(spawnCollider[random], spawns[random]);
                //instancia un perro aleatorio el en centro del spawn
                GameObject generatedDog = Instantiate(dogs[Random.Range(0, dogs.Length)],center,Quaternion.identity);

                generatedDog.transform.parent = spawns[random].transform;

                yield return new WaitForSeconds(waitDogs);
            }

            yield return new WaitForSeconds(waveTime);

        }

    }

    public void calcPoints(int points, float extraT)
    {
        extraTime += extraT;
        totalPoints += points;
        setPointsUI();
    }

    public bool youLost()
    {
        return gameOver;
    }

    public float getTime()
    {
        return remainingTime;
    }

    public int getPoints()
    {
        return totalPoints;
    }

    public void setPointsUI()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Puntos: ");
        sb.Append(totalPoints);

        //añadir puntos a UI
        pointsUI.text = sb.ToString();
    }

    public Vector3 getCenter(BoxCollider bc, GameObject go)
    {
        Vector3 center = bc.center+go.transform.position;
        return center;

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnDogs());
    }

    public void isGameOver()
    {
        //comprobar si se acaba el tiempo
        if (remainingTime <= 0f)
        {
            gameOver = true;
            remainingTime = 0f;
        }
        else
        {
            calcTime();
        }

        if (gameOver)
        {
            remainingTime = 0f;

            gameOverUI.gameObject.SetActive(true);
            resetUI.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //reiniciar y volver a la escena de juego
            if (Input.GetKeyDown(KeyCode.R))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        isGameOver();
    }
}
