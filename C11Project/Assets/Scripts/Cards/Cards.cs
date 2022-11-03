using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Cards : MonoBehaviour
{
    /// <summary>
    /// ˳���ճ�ʼͼ����ʾ˳����
    /// mid,right,left
    /// </summary>
    public enum CardsType 
    {
        jetpack,//��������
        timeGel,//ʱ�佺��
        raptorSwoop//���ݸ���
    }
    //��ǰѡ�п���
    
    public static Cards instance;

    [Header("��������"), Tooltip("�����е��������")]
    public GameObject player; //����������ԭ����ʹ����Ԥ��������
    [Tooltip("��ұ�ǩ")]
    public string playerTag;
    [Tooltip("���˱�ǩ")]
    public string enemyTag;
    [Tooltip("�����ǩ")]
    public string trapTag;

    [Header("������������"),Tooltip("�����Я������")]
    public int cardMaxNum = 6;
    [Tooltip("��������ʣ������")]
    public int jetpackRemainCardNum;
    [Tooltip("ʱ�佺��ʣ������")]
    public int timeGelRemainCardNum;
    [Tooltip("���ݸ���ʣ������")]
    public int raptorSwoopRemainCardNum;

    [Header("�޿���ʱ�Ĳ�����ʾ"),Tooltip("�޿���UI")]
    public TextMeshProUGUI noCardTip;
    [Tooltip("��������������ʾ")]
    public string jetpackText;
    [Tooltip("ʱ�佺��������ʾ")]
    public string timeGelText;
    [Tooltip("���ݸ���������ʾ")]
    public string raptorSwoopText;

    [Header("��������"), Tooltip("ʹ�ü��ܿ��Ƶİ�������")]
    public KeyCode specialCardKeyCode;
    [Tooltip("ʹ��ͨ��Ч�����Ƶİ�������")]
    public KeyCode generalCardKeyCode;

    [Header("��������"), Tooltip("��������")]
    public string jetpackName;
    [Tooltip("ʱ�佺��")]
    public string timeGelName;
    [Tooltip("���ݸ���")]
    public string raptorSwoopName;
    [Tooltip("��ǰѡ�п���")]
    public CardsType curSelectCard;
    private void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        LoadGameObject();
    }
    private void Update()
    {
        CardNumUIShow();
        UpdataCardNum();
        NoCardsUITip();
        GetKeyCodeToUseCard();
    }
    /// <summary>
    /// ���һ���µĿ��Ʋ����»�õĿ�����Ϊѡ�еĿ���
    /// </summary>
    /// <param name="cardName">����ӿ�������</param>
    public void AddOneCard(string cardName)
    {
        if (cardName == jetpackName && jetpackRemainCardNum < cardMaxNum)
        {
            jetpackRemainCardNum++;
            AddCardChangeAnimation(jetpackName);
        }

        if (cardName == timeGelName && timeGelRemainCardNum < cardMaxNum)
        {
            timeGelRemainCardNum++;
            AddCardChangeAnimation(timeGelName);
        }

        if (cardName == raptorSwoopName && raptorSwoopRemainCardNum < cardMaxNum)
        {
            raptorSwoopRemainCardNum++;
            AddCardChangeAnimation(raptorSwoopName);
        }
    }
    /// <summary>
    /// �¿�����ӵ��л�����
    /// </summary>
    /// <param name="cardName">����ӿ�������</param>
    void AddCardChangeAnimation(string cardName)
    {
        if (cardName == jetpackName)
        {
            if (curSelectCard == CardsType.timeGel) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnRightName);
            if (curSelectCard == CardsType.raptorSwoop) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnLeftName);
        }

        if (cardName == timeGelName)
        {
            if (curSelectCard == CardsType.raptorSwoop) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnRightName);
            if (curSelectCard == CardsType.jetpack) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnLeftName);
        }

        if (cardName == raptorSwoopName)
        {
            if (curSelectCard == CardsType.jetpack) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnRightName);
            if (curSelectCard == CardsType.timeGel) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnLeftName);
        }
    }
    void LoadGameObject()
    {
        if (playerTag == "") playerTag = "Player";
        if (!player) player = GameObject.FindGameObjectWithTag(playerTag);

        if (enemyTag == "") enemyTag = "Enemy";
        if (trapTag == "") trapTag = "Trap";
    }
    /// <summary>
    /// ���ƿ���ʣ���������������ֵ
    /// </summary>
    void UpdataCardNum()
    {
        if (jetpackRemainCardNum > cardMaxNum) jetpackRemainCardNum = cardMaxNum;
        if (timeGelRemainCardNum > cardMaxNum) timeGelRemainCardNum = cardMaxNum;
        if (raptorSwoopRemainCardNum > cardMaxNum) raptorSwoopRemainCardNum = cardMaxNum;
    }
    /// <summary>
    /// ����ʣ��������ʾ
    /// </summary>
    void CardNumUIShow()
    {
        //Debug.Log(CardsAnimation.instance.images);
        foreach (var image in CardsAnimation.instance.images)
        {
            TextMeshProUGUI childText = image.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if(image.name == jetpackName)
            {
                childText.text = jetpackRemainCardNum.ToString();
                CardColorChange(image, jetpackRemainCardNum);
            }

            if (image.name == timeGelName)
            {
                childText.text = timeGelRemainCardNum.ToString();
                CardColorChange(image, timeGelRemainCardNum);
            }

            if (image.name == raptorSwoopName)
            {
                childText.text = raptorSwoopRemainCardNum.ToString();
                CardColorChange(image, raptorSwoopRemainCardNum);
            }
        }
    }
    /// <summary>
    /// ��ʣ������Ϊ0�Ŀ��ƿ�����
    /// </summary>
    /// <param name="image">��ǰ����image</param>
    /// <param name="curRemainCardNum">��ǰ��ʣ������</param>
    void CardColorChange(Image image,int curRemainCardNum)
    {
        if (curRemainCardNum <= 0) image.color = Color.gray;
        else image.color = Color.white;
    }
    /// <summary>
    /// �޿��ƿ���ʱ��UI��ʾ
    /// </summary>
    void NoCardsUITip()
    {
        if(jetpackRemainCardNum == 0 && timeGelRemainCardNum == 0 && raptorSwoopRemainCardNum == 0)
        {
            noCardTip.gameObject.SetActive(true);
            switch (curSelectCard) 
            {
                case CardsType.jetpack:
                    noCardTip.text = jetpackText;
                    break;
                case CardsType.timeGel:
                    noCardTip.text = timeGelText;
                    break;
                case CardsType.raptorSwoop:
                    noCardTip.text = raptorSwoopText;
                    break;
            }
        }
        else
        {
            noCardTip.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ��ȡ������Ϣʹ�ÿ���
    /// </summary>
    void GetKeyCodeToUseCard()
    {
        //���⿨��Ч��
        if (Input.GetKeyDown(specialCardKeyCode))
        {
            //ʹ����������
            if (curSelectCard == CardsType.jetpack && jetpackRemainCardNum > 0)
            {
                jetpackRemainCardNum--;
                Jetpack.instance.UseJetpack();
                Debug.Log("ʹ����������");
            }
            //ʹ��ʱ�佺��
            if (curSelectCard == CardsType.timeGel && timeGelRemainCardNum > 0)
            {
                timeGelRemainCardNum--;
                TimeGel.instance.UseTimeGel();
                Debug.Log("ʹ��ʱ�佺��");
            }
            //ʹ�����ݸ���
            if (curSelectCard == CardsType.raptorSwoop && raptorSwoopRemainCardNum > 0)
            {
                raptorSwoopRemainCardNum--;
                RaptorSwoop.instance.UseRaptorSwoop();
                Debug.Log("ʹ�����ݸ���");
            }
        }
        //ͨ��Ч��
        if (Input.GetKeyDown(generalCardKeyCode))
        {
            GeneralEffect.instance.UseGeneralEffect();
            Debug.Log("ʹ��ͨ��Ч��");
            if (curSelectCard == CardsType.jetpack && jetpackRemainCardNum > 0)
            {
                jetpackRemainCardNum--;
            }
            if (curSelectCard == CardsType.timeGel && timeGelRemainCardNum > 0)
            {
                timeGelRemainCardNum--;
            }
            if (curSelectCard == CardsType.raptorSwoop && raptorSwoopRemainCardNum > 0)
            {
                raptorSwoopRemainCardNum--;
            }
        }
    }
}
