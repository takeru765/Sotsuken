using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Trophy_4 : MonoBehaviour
{
    public GameObject PlaTrophy_Button;
    public Counter_test TrashPoint;

    public Sprite trashSprite;
    private Image image;
    public int Btrash_alumi;//引用した数値を入れる変数
    public int Btrash_steal;
    public int Btrash_pet;
    public int Btrash_pla;
    public int Btrash_paper;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;//ボタン機能を無効にする

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

        if (Btrash_steal >= 20)//アルミ２０個
        {
            Button btn = GameObject.Find("Stealbadge").GetComponent<Button>();
            btn.interactable = true;
            //CanTrophy_Button.SetActive(true);

            image.sprite = trashSprite;//画像を変える→IF文で差し込め！

            //Debug.Log("trophy起動");
        }
    }
}
