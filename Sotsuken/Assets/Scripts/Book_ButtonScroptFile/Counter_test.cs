using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Counter_test : MonoBehaviour
{
    public int alumi;//それぞれごみのポイントを仮定した数値
    public int steal;
    public int pet;
    public int pla;
    public int paper;


    public Trophy_Flag flag_script;
    // Start is called before the first frame update
    void Start()
    {
       

    }

    public void Onclick()
    {
        alumi += 1;
        steal += 1;
        pet += 1;
        pla += 1;
        paper += 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (paper == 0)
        {
            Debug.Log("ゼロだよ");
        }
        else if (paper == 3)
        {
            Debug.Log("3カウント！");
        }*/


    }

}
