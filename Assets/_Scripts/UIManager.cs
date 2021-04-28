using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    #region Inspector Properties
    [SerializeField] Button playAgainButton;
    [SerializeField] Text gameOverText;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject UIcon;
    #endregion

    private void Awake()
    {
        if (UIManager.Instance == null)
        {
            UIManager.Instance = this.GetComponent<UIManager>();

        }
        else if (UIManager.Instance != null && UIManager.Instance != this)
        {
            Destroy(gameObject);
            return;

        }
        DontDestroyOnLoad(this);

        pausePanel = GameObject.Find("PausePanel"); 
        pausePanel.GetComponent<Image>().enabled = true; pausePanel.SetActive(false) ;
       

    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            Pause();
    }


    public void Pause()
    {
        if (pausePanel.activeSelf)
            pausePanel.SetActive(false);
        else
            pausePanel.SetActive(true);

        Time.timeScale = (pausePanel.activeSelf) ? 0 : 1;
    }

    public void ActivateUiCon(string IconName)
    {
        UIcon = GameObject.Find(IconName);
        UIcon.gameObject.GetComponent<Image>().enabled =true;
    }

    public void DesaactivateUiCon(string IconName)
    {
        UIcon = GameObject.Find(IconName);
        UIcon.gameObject.GetComponent<Image>().enabled = false;
    }
}
