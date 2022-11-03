using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorSwoop : MonoBehaviour
{
    public static RaptorSwoop instance;
    [Header("��������"), Tooltip("�Զ��������ݸ�����������Χ����ҽŵ�")]
    public bool automaticSetPosition;
    [Tooltip("�����")]
    public LayerMask layerGround;

    [Header("״̬����"), Tooltip("����ʹ�����ݸ���")]
    public bool isUsingRaptorSwoop;
    [Tooltip("���ﴦ�ڵ���")]
    public bool isOnGround;
    [Tooltip("����һ����Χ�ĵ��˺�����")]
    public bool isDestroyEnemyAndTrap;
    [Tooltip("�Ƿ��Ѿ�����һ��ƽ̨")]
    public bool isPastOnePlane;
    [Tooltip("������ǰ����")]
    public bool isEjectionForward;

    [Header("���ݸ������"),Tooltip("���ݸ����ٶ�"), Range(100, 2000)]
    public float raptorSwoopSpeed;
    [Tooltip("�����ٶ�"), Range(1,50)]
    public float ejectionSpeed;
    [Tooltip("����ʱ��"), Range(0,10)]
    public float ejectionTime;
    [Tooltip("���ݸ���ʱ�������Ʒ��ƽ̨��ǩ�б�")]
    public List<string> destroyTagList;
    [Tooltip("���ݸ���ʱ�ɴ�Խ��ƽ̨��ǩ�б�")]
    public List<string> passingPlaneTagList;
    
    //bool pastDestroyPlane;
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
        if (isUsingRaptorSwoop)
        {
            OnGroundCheck();
            if (isEjectionForward) EjectionForward();
            //���ٴ�ֱ����
            RapidDown();
            //���ʱ������һ����Χ�ڵĵ��˺�������ƻ����ƻ���ƽ̨
            DestroyEnemyAndTrap();
            if (automaticSetPosition)
            {
                RaptorSwoopDestroyRange.instance.SetInitPosition();
            }

            if (isOnGround && isPastOnePlane)
            {
                //����ͣ������ǰ����
                StartEjectionForward();
                Invoke("EndRaptorSwoop", ejectionTime);
            }
        }
        else
        {
            ResetParam();
        }
    }
    public void UseRaptorSwoop()
    {
        isUsingRaptorSwoop = true;
    }
    /// <summary>
    /// ���ٴ�ֱ����
    /// </summary>
    void RapidDown()
    {
        Debug.Log("���ڽ������ݸ����������");
        Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.down * Time.deltaTime * raptorSwoopSpeed + Physics2D.gravity;
    }
    /// <summary>
    /// ���ʱ������һ����Χ�ڵĵ��˺�����
    /// ���ƻ����ƻ���ƽ̨
    /// </summary>
    void DestroyEnemyAndTrap()
    {
        isDestroyEnemyAndTrap = true;
    }
    void EjectionForward()
    {
        Debug.Log("����ǰ��");
        //Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * ejectionSpeed;
        //Cards.instance.player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * ejectionSpeed);
        Cards.instance.player.transform.Translate(Vector2.right * Time.deltaTime * ejectionSpeed);
        //Debug.Log(Cards.instance.player.GetComponent<Rigidbody2D>().velocity);
    }
    void StartEjectionForward()
    {
        isEjectionForward = true;
    }
    void EndRaptorSwoop()
    {
        isUsingRaptorSwoop = false;
        Debug.Log("�������ݸ���");
        return;
    }
    void ResetParam()
    {
        isDestroyEnemyAndTrap = false;
        isEjectionForward = false;
        isPastOnePlane = false;
    }
    void OnGroundCheck()
    {
        isOnGround = Cards.instance.player.GetComponent<Collider2D>().IsTouchingLayers(layerGround);
    }
}
