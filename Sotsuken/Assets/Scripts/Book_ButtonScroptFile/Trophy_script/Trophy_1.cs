using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Trophy_1 : MonoBehaviour
{
    public GameObject CanTrophy_Button;
    public Counter_test TrashPoint;

    public int Trophy_ID;
    public Sprite trashSprite;
    private Image image;
    public int Btrash_alumi;//���p�������l������ϐ�
    public int Btrash_steal;
    public int Btrash_pet;
    public int Btrash_pla;
    public int Btrash_paper;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;//�{�^���@�\�𖳌��ɂ���

        image = GetComponent<Image>();
        //CanTrophy_Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Btrash_alumi = TrashPoint.alumi;
        Btrash_steal = TrashPoint.steal;
        Btrash_pet = TrashPoint.pet;
        Btrash_pla = TrashPoint.pla;
        Btrash_paper = TrashPoint.paper;

        if ((Btrash_alumi >= 1) && (Btrash_steal >= 1))//�A���~�ƃX�`�[�����o�^���ꂽ�Ȃ��...
        {
            Button btn = GameObject.Find("CanTrophy_Button").GetComponent<Button>();
            btn.interactable = true;
            //CanTrophy_Button.SetActive(true);

            image.sprite = trashSprite;//�摜��ς��遨IF���ō������߁I

            Debug.Log("trophy�N��");
        }

    }
}
