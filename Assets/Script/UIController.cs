using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{

    //private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private SettingsPopup settingsPopup;
    private int popupsActive = 0;
    // Start is called before the first frame update
    void Start()
    {

       UpdateHealth(1.0f);
       OnHealthChanged(1.0f);

    }
    private void Awake()
    {
       Messenger<float>.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
       Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
       Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    private void OnPopupClosed()
    {
        Debug.Log(this + ".OnPopupClosed(): " + popupsActive );

        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
        }
    }

    private void OnPopupOpened()
    {
        Debug.Log(this + ".OnPopupOpened(): " + popupsActive);
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }
        popupsActive++;

    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }
   
    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Escape) && popupsActive == 0)
        {
            optionsPopup.Open();
        }
       
    }

    public void OnHealthChanged(float healthPercentage)
    {
        UpdateHealth(healthPercentage);
        healthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }
    public void UpdateHealth(float health)
    {
        healthBar.fillAmount = health;
    }

    // update score display
    public void UpdateScore(int newScore)
    {
        scoreValue.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Messenger.Broadcast(GameEvent.GAME_ACTIVE);
            Time.timeScale = 1; // unpause the game
            Cursor.lockState = CursorLockMode.Locked; // lock cursor at center
            Cursor.visible = false; // hide cursor
            crossHair.gameObject.SetActive(true); // show the crosshair
        }
        else
        {
            Messenger.Broadcast(GameEvent.GAME_INACTIVE);
            Time.timeScale = 0; // pause the game
            Cursor.lockState = CursorLockMode.None; // let cursor move freely
            Cursor.visible = true; // show the cursor
            crossHair.gameObject.SetActive(false); // turn off the crosshair
        }
    }
}
