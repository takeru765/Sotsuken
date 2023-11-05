using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //自身を非表示にする。メッセージポップアップなどに。
    public void CloseItself()
    {
        this.gameObject.SetActive(false);
    }
}
