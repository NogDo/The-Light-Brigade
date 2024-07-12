using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamagePool : MonoBehaviour
{
    Queue<GameObject> queueDamagetextpool;
    public GameObject oDamagetextprefab; //Instantiate 메서드로 복제할 프리펩을 담을 변수
    public Canvas canvasEnemydamage;
    public Vector3 v3Damageoffset = new Vector3(0, 2.4f, 0);
    int nDamagetextcount;

    void Start()
    {
        nDamagetextcount = 10;
        queueDamagetextpool = new Queue<GameObject>();
        canvasEnemydamage = GameObject.Find("Enemy Damage Canvas").GetComponent<Canvas>();
        SetDamageText();

        for (int i = 0; i < nDamagetextcount; i++)
        {
            queueDamagetextpool.Enqueue(oDamagetextprefab);

        }
    }

    public void OutDamageText()
    {
        queueDamagetextpool.Dequeue().transform.GetChild(0).gameObject.SetActive(true);
    }

    void SetDamageText()
    {
        canvasEnemydamage = GameObject.Find("Enemy Damage Canvas").GetComponent<Canvas>();
        // 데미지 텍스트 인스턴스 생성
        GameObject damageText = Instantiate<GameObject>(oDamagetextprefab, canvasEnemydamage.transform);
        var _damageText = damageText.GetComponent<UIDamageText>();
        _damageText.trEnemy = gameObject.transform;
        _damageText.v3Offset = v3Damageoffset;
    }
}
