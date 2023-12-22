using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMana : MonoBehaviour
{
    [SerializeField] GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
