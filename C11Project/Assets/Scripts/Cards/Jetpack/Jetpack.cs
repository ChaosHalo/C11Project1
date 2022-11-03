using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public static Jetpack instance;
    
    [Tooltip("����㼶")]  
    public LayerMask layerGroundMask;   //��Ҫ��ѩ�ƹ�ͨ

    [Header("״̬����"), Tooltip("���ڻ���")]
    public bool isGliding;
    [Tooltip("����ʹ����������")]
    public bool isUsingJetpack;

    [Header("������������"), Tooltip("����λ�ƾ���"), Range(1,50)]
    public float upDistance;
    [Tooltip("���������ٶ�"), Range(100,2000)]
    public float jetpackSpeed;
    [Tooltip("��ؽ���������"), Range(1, 50)]
    public float reachGroundDistance;
    [Tooltip("�������¿���")]
    public KeyCode controllDownGlidingKeycode;
    [Tooltip("�������¿����ٶ�"), Range(50, 2000)]
    public float downGlidingSpeed;

    [Header("Debug����")]
    [SerializeField, Tooltip("�����ʼλ��")] float originY;
    [SerializeField, Tooltip("�����������λ��")] float targetY;
    [SerializeField, Tooltip("��ҵ�ǰλ��")] float curPlayerY;
    [SerializeField, Tooltip("�������������ʾ���ƿ���")] bool switchRayReachGround;
    [SerializeField, Tooltip("�������������ʱ����")] bool switchReachGroundFrozen;
    [SerializeField, Tooltip("Debug�ı���ʾ")] bool switchDebugText;

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
        DebugController();
        //ʹ������������ʼ����λ��
        if (isUsingJetpack)
        {
            Debug.Log("����ʹ��������������");
            UpwardDisplacement();
            //����ָ��λ�ƾ���
            if (IsReachTagetDistance())
            {
                Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
                Debug.Log("�ѵ���ָ���߶�");
                isUsingJetpack = false;
                isGliding = true;
            }
        }
        
        //��ʼ����
        if (isGliding)
        {
            //�����������������״̬
            if (IsComingToGround())
            {
                Debug.Log("�������");
                isGliding = false;
            }
            //�������¿���
            ControllDownGliding();
        }
    }
    
    /// <summary>
    /// Debug������
    /// </summary>
    void DebugController()
    {
        if (switchRayReachGround)
        {
            ShowReachGroundRay();
        }
        if (switchReachGroundFrozen && isGliding && IsComingToGround())
        {
            FreezePlayer();
        }

        if (switchDebugText)
        {
            Debug.unityLogger.filterLogType = LogType.Log; //��ʾ����
        }
        else
        {
            Debug.unityLogger.filterLogType = LogType.Error; //ֻ��ʾError + Exception
        }
    }
    /// <summary>
    /// ʹ����������
    /// </summary>
    public void UseJetpack()
    {
        isUsingJetpack = true;
        originY = Cards.instance.player.transform.position.y;
        targetY = originY + upDistance;
        Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
    }
    /// <summary>
    /// ����λ��һ�ξ���
    /// </summary>
    void UpwardDisplacement()
    {
        Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.up * jetpackSpeed * Time.deltaTime;
    }
    /// <summary>
    /// �Ƿ�ɵ�ָ���߶�
    /// </summary>
    /// <returns></returns>
    bool IsReachTagetDistance()
    {
        curPlayerY = Cards.instance.player.transform.position.y;
        return curPlayerY >= targetY;
    }
    /// <summary>
    /// �Ƿ񵽴���ؽ���������
    /// </summary>
    bool IsComingToGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(Cards.instance.player.transform.position, Vector2.down, reachGroundDistance, layerGroundMask);
        
        return raycastHit2D;
    }
    /// <summary>
    /// ���а����¼���������
    /// </summary>
    void ControllDownGliding()
    {
        if (Input.GetKey(controllDownGlidingKeycode))
        {
            Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Cards.instance.player.GetComponent<Rigidbody2D>().velocity + Vector2.down * downGlidingSpeed * Time.deltaTime;
        }
    }
    /// <summary>
    /// ��ʾ��ؽ����������
    /// </summary>
    void ShowReachGroundRay()
    {
        Debug.DrawRay(Cards.instance.player.transform.position, Vector2.down * reachGroundDistance, Color.red);
    }
    
    /// <summary>
    /// �������λ��
    /// </summary>
    void FreezePlayer()
    {
        Cards.instance.player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }

}
