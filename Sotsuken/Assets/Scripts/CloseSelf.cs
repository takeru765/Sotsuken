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

    //���g���\���ɂ���B���b�Z�[�W�|�b�v�A�b�v�ȂǂɁB
    public void CloseItself()
    {
        this.gameObject.SetActive(false);
    }
}
