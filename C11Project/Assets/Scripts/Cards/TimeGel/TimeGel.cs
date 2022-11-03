using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGel : MonoBehaviour
{
    [Header("��������"),Tooltip("��������Ԥ����")]
    public GameObject bubblePrefab;
    [Tooltip("�����������٣�ֵԽ��Խ��"),Range(1,10)]
    public float bubbleSpeed;
    [Tooltip("������������λ��(��ֵx���ң���ֵy����)")]
    public Vector3 bubblePos = new Vector3(1,0,0);
    [Tooltip("���ݼ���Ч��,ֵԽ�����Ч��Խ����"),Range(0.1f,1)]
    public float decelerationRatio;

    public static TimeGel instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UseTimeGel()
    {
        GameObject newBubble = Instantiate(bubblePrefab,Cards.instance.player.transform.localPosition + bubblePos, Quaternion.identity);
    }
}
