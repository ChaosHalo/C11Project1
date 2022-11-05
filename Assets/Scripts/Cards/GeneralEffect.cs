using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffect : MonoBehaviour
{
    public static GeneralEffect instance;

    [Header("�ӵ�ʱ�����"), Tooltip("�ӵ�ʱ�����Ч��,ֵԽ�����Ч��Խ����"), Range(0.1f, 1)]
    public float decelerationRatio;
    [Tooltip("�ӵ�ʱ�䳤��,ֵԽ�����Ч��Խ����"), Range(0.1f, 5)]
    public float bulletTime;
    
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
    /// <summary>
    /// ʹ��ͨ��Ч���ӵ�ʱ�䣬�������������һ��ʱ��
    /// Ŀǰ�Ѿ���ӵ����壺player��enermy
    /// </summary>
    public void UseGeneralEffect()
    {
        ResetJumpTime();

        StartCoroutine(DecelerationBubbleSpeed());
        StartCoroutine(PlayerBulletTime());
        StartCoroutine(EnemyBulletTime());
    }
    /// <summary>
    /// ������Ծ����
    /// ��Ҫ��xingo��ͨ
    /// </summary>
    void ResetJumpTime()
    {
        
    }
    /// <summary>
    /// ʱ�佺�ҵļ����ݵ��ӵ�ʱ��
    /// </summary>
    /// <returns></returns>
    IEnumerator DecelerationBubbleSpeed()
    {
        TimeGel.instance.bubbleSpeed *= decelerationRatio;
        yield return new WaitForSeconds(bulletTime);
        TimeGel.instance.bubbleSpeed /= decelerationRatio;
    }
    /// <summary>
    /// ���˵��ӵ�ʱ��Э��
    /// �������Ҫ��ѩ�ɹ�ͨ
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator EnemyBulletTime()
    {
        Enemy.instance.speed *= decelerationRatio;
        yield return new WaitForSeconds(bulletTime);
        Enemy.instance.speed /= decelerationRatio;
    }
    /// <summary>
    /// ��ҵ��ӵ�ʱ��Э��
    /// �������Ҫ��xingo��ͨ
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerBulletTime()
    {
        PlayerController.instance.speed *= decelerationRatio;
        yield return new WaitForSeconds(bulletTime);
        PlayerController.instance.speed /= decelerationRatio;
    }
}
