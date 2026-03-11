using System;
using UnityEngine;

[Serializable]
public struct UIPage
{
    public string name;
    public GameObject[] objs;
}

public enum GAME_STATUS
{
    MAIN_MENU = 0,
    SCREEN_1,
    SCREEN_2,
    SCREEN_3
}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    public GAME_STATUS m_status;
    [Space]
    [SerializeField] UIPage[] screens;

    public GAME_STATUS GetCurrentStatus => m_status;
    
    public Action<GAME_STATUS> OnGameStatusChange;
    public Action<int, int> OnChangeStarted;
    public Action<int, int> OnChangeEnded;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
#if UNITY_EDITOR
        ChangeStatus(m_status);
#else
        ChangeStatus(GAME_STATUS.MAIN_MENU);
#endif
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeStatus(GAME_STATUS.MAIN_MENU);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangeStatus(GAME_STATUS.SCREEN_1);
        }
    }

    public void HideObjs()
    {
        foreach (var screen in screens)
        {
            foreach (var child in screen.objs)
            {
                child.SetActive(false);
            }
        }
    }

    public void ShowItems(int p_listIndex)
    {
        var objs = screens[p_listIndex].objs;

        foreach (var child in objs)
        {
            child.SetActive(true);
        }
    }

    public void ChangeStatus(GAME_STATUS p_status = GAME_STATUS.MAIN_MENU)
    {
        OnChangeStarted?.Invoke((int)m_status, (int)p_status);
        m_status = p_status;
        OnGameStatusChange?.Invoke(p_status);
        HideObjs();
        ShowItems((int)p_status);
    }

    public void ChangeStatus(int p_statusIndex)
    {
        ChangeStatus((GAME_STATUS)p_statusIndex);
    }

    private void OnDrawGizmosSelected()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Main Menu"))
        {
            Debug.Log("Selected");
        }
    }

}