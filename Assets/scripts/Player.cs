using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    int jumpFlag = 0;
    float speed = 2.0f;
    int score = 0;
    public Text ScoreText;
    public Text TimeText;
    float CountTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),
                                0,
                                Input.GetAxisRaw("Vertical"));
        rigid.AddForce(vec*speed, ForceMode.Impulse);

        if(Input.GetKeyDown(KeyCode.Space) && jumpFlag == 1)
        {
            rigid.AddForce(new Vector3(0, 6000, 0), ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed*=2;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed*=0.5f;
        }

        if(Input.GetKeyDown(KeyCode.R)||this.transform.position.y<-30.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        ScoreText.text = "Score: "+ score;

        CountTime += Time.deltaTime;
        int secTime = (int)CountTime;
        TimeText.text = string.Format("{0:D2}:{1:D2}", secTime/60, secTime%60);

    }

    private void OnCollisionEnter(Collision collsion)
    {
        if(collsion.transform.tag == "ground")
        {
            jumpFlag = 1;
        }
        
    }

    private void OnCollisionExit(Collision collsion)
    {
        if(collsion.transform.tag == "ground")
        {
            jumpFlag = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Cube")
        {
            score++;
            other.gameObject.SetActive(false);
        }
        else if(other.transform.tag == "Finish")
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        
    }
}
