using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playercontroller : MonoBehaviour
{
    Rigidbody rb;
    float speed = 8;

    public Text txtscore;
    int score;
    public ParticleSystem psplayer;
    public ParticleSystem psenemy;
    public GameObject panelgameover;
    public GameObject panellevelcompleted;
    bool isgameover;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < 2)
        {
            psplayer.Stop();

        }
        else
        {
            if(!psplayer.isPlaying)
            {
                psplayer.Play();
            }
        }

    }
    private void FixedUpdate()
    {
        if(!isgameover)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin") 
        {
            Destroy(other.gameObject);
            score++;
            txtscore.text = "Score : " + score;
        }
        if(other.gameObject.tag == "enemy")
        {
            isgameover = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            psenemy.Play();
            Destroy(other.gameObject, 2.0f);
            panelgameover.SetActive(true);
        }
        if(other.gameObject.tag == "finish")
        {            
            panellevelcompleted.SetActive(true);   
        }
    }
    public void playAgainUI()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }
    public void playAgain1UI()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

