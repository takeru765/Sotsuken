using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_DevWin : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject developWindow;

    public void CloseDevWin()
    {
        developWindow.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
