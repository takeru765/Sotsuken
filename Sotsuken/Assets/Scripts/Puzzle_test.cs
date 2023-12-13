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
    GameObject preFrame;//preframe �h���b�O���̘g preposition �h���b�O���̍��W

    private void Awake()
    {
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)//�h���b�O�O�̃N���b�N����
    {

        prePosition = transform.position;//�h���b�O�O�̈ʒu���L�����Ă���

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);//raycast ���ʃr�[�����o�� �����������̂ɂ����

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("image"))//image �t���[���g�̂���
            {
                preFrame = hit.gameObject;//��������W��Ԃ�
            }

        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = Input.mousePosition;//�X�N���[�����W,canvasRect.sizeDelta.x�̓L�����o�X���W
        var magnification = canvasRect.sizeDelta.x / Screen.width;//�X�N���[���T�C�Y���L�����o�X�T�C�Y�Ȃ炻�̂܂ܑ������΂������A�T�C�Y���Ⴄ���ߒ�������B

        mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2;
        mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2;
        mousePos.z = transform.localPosition.z;//���̂R�ō��W�̋N�_�̂������������B

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
                 transform.position = hit.gameObject.transform.position;//�ړ���̘g�ɍ��킹��i���ꂪ�Ȃ��ƁA���������̏�ŃI�u�W�F�N�g����~���Ă��܂��B
                 flg = false;
             }

         }

         if (flg)
         {
             transform.position = prePosition;//����ւ��t���O��true�Ȃ�A���̈ʒu�ɖ߂��B
         }

   
    }
}


