using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamagePool : MonoBehaviour
{
    Queue<GameObject> queueDamagetextpool;
    public GameObject oDamagetextprefab; //Instantiate �޼���� ������ �������� ���� ����
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
        // ������ �ؽ�Ʈ �ν��Ͻ� ����
        GameObject damageText = Instantiate<GameObject>(oDamagetextprefab, canvasEnemydamage.transform);
        var _damageText = damageText.GetComponent<UIDamageText>();
        _damageText.trEnemy = gameObject.transform;
        _damageText.v3Offset = v3Damageoffset;
    }
}
