using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_OpenWindow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject devBox;

    public void OnClick()
    {
        devBox.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
