using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle_test : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update

    private Vector2 prePosition;
    RectTransform canvasRect;
    GameObject preFrame;//preframe ドラッグ元の枠 preposition ドラッグ元の座標

    private void Awake()
    {
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)//ドラッグ前のクリック処理
    {

        prePosition = transform.position;//ドラッグ前の位置を記憶しておく

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);//raycast 判別ビームを出す あたったものによって

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("image"))//image フレーム枠のこと
            {
                preFrame = hit.gameObject;//今いる座標を返す
            }

        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = Input.mousePosition;//スクリーン座標,canvasRect.sizeDelta.xはキャンバス座標
        var magnification = canvasRect.sizeDelta.x / Screen.width;//スクリーンサイズ＝キャンバスサイズならそのまま代入すればいいが、サイズが違うため調整する。

        mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2;
        mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2;
        mousePos.z = transform.localPosition.z;//この３つで座標の起点のずれを解消する。

        transform.localPosition = mousePos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
         bool flg = true;

         var raycastResults = new List<RaycastResult>();
         EventSystem.current.RaycastAll(eventData, raycastResults);

         foreach (var hit in raycastResults)
         {
             if (hit.gameObject.CompareTag("image"))
             {
                 transform.position = hit.gameObject.transform.position;//移動先の枠に合わせる（これがないと、離したその場でオブジェクトが停止してしまう。
                 flg = false;
             }

         }

         if (flg)
         {
             transform.position = prePosition;//入れ替えフラグがtrueなら、元の位置に戻す。
         }

   
    }
}


