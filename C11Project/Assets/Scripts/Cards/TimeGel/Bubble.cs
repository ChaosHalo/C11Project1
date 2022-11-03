using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.Play("BubbleEnlager");
    }

    // Update is called once per frame
    void Update()
    {
        bubbleMove();
    }
    void bubbleMove()
    {
        transform.Translate(Vector2.right * TimeGel.instance.bubbleSpeed * Time.deltaTime);
    }
    /// <summary>
    /// �����������Χ����������¼�
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    /// <summary>
    /// ����Ч������ʵ��
    /// ��Ҫ��ѩ�ƹ�ͨtag����layer
    /// �������enermyTag��
    /// ��Ҫ���˽ű�����(enemy)���ٶ���(speed)
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == Cards.instance.enemyTag)
        {
            Enemy.instance.speed *= TimeGel.instance.decelerationRatio;
            //collision.GetComponent<Animator>().speed *= TimeGel.instance.decelerationRatio;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Cards.instance.enemyTag)
        {
            Enemy.instance.speed /= TimeGel.instance.decelerationRatio;
            
            //collision.GetComponent<Animator>().speed /= TimeGel.instance.decelerationRatio;
        }
    }
}
