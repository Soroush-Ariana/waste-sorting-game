using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _MyFiles._Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject panelResult;
        public GameObject panelPause;

        public int countAll;

        public GameObject youWin, youLost;
        public GameObject btnNextLevel, btnFinish, btnAgain;
        
        public int countTrue;
        public int countFalse;
        public Text txtCountTrue;
        public Text txtCountFalse;
        public Text txtTotalTimer;
        
        public float timer, timerLock;
        public Text txtTimer;
        public Text[] txtTimerLock;
        private bool stopTimer = true;
        private bool stopTimerLock = false;

        public Sprite[] spPlastic;
        public Image[] imgWaste;
        public BoxCollider2D[] goWaste;
        
        private string nameScene;

        public int bestRecord;

        public Image imgCharacter;
        public Sprite spNormal, spHappy, spSad;


        private void Start()
        {
            if (PlayerPrefs.GetInt("CountAudio") == 0)
            {
                GameObject.Find("Audio Correct").GetComponent<AudioSource>().mute = false;
                GameObject.Find("Audio Wrong").GetComponent<AudioSource>().mute = false;
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = false;
            }
            else if (PlayerPrefs.GetInt("CountAudio") == 1)
            {
                GameObject.Find("Audio Correct").GetComponent<AudioSource>().mute = true;
                GameObject.Find("Audio Wrong").GetComponent<AudioSource>().mute = true;
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
            }
            
            nameScene = SceneManager.GetActiveScene().name;

            if (nameScene == "Level1")
            {
                stopTimer = false;
            }
            else if (nameScene == "Level2")
            {
                Invoke(nameof(OnPlastic), 5f);
            }
        }

        private void OnPlastic()
        {
            for (int i = 0; i < imgWaste.Length; i++)
            {
                imgWaste[i].sprite = spPlastic[0];
                
                var scale = imgWaste[i].transform.localScale;
                scale.x = 0.14f;
                scale.y = 0.14f;
                scale.z = 0.14f;
                imgWaste[i].transform.localScale = scale;
                
                imgWaste[i].SetNativeSize();
                var colider = imgWaste[i].GetComponent<BoxCollider2D>().size;
                colider.x = 500f;
                colider.y = 800f;
                imgWaste[i].GetComponent<BoxCollider2D>().size = colider;
            }
        }

        private void Update()
        {
            if (nameScene == "Level2")
            {
                if (stopTimerLock == false)
                {
                    timerLock -= 1 * Time.deltaTime;
                    txtTimerLock[0].text = timerLock.ToString("00");
                    
                    if (timerLock <= 0)
                    {
                        stopTimerLock = true;
                        for (int i = 0; i < goWaste.Length; i++)
                        {
                            goWaste[i].enabled = true;
                        }
                        txtTimerLock[0].gameObject.SetActive(false);
                        txtTimer.gameObject.SetActive(true);

                        stopTimer = false;
                    }
                }
            }
            
            if (stopTimer == false)
            {
                timer -= 1 * Time.deltaTime;
                txtTimer.text = timer.ToString("00");
            }
            if (timer <= 0)
            {
                stopTimer = true;
                youLost.SetActive(true);
                btnAgain.SetActive(true);
                ShowResult();
            }
        }

        public void ShowResult()
        {
            GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
            
            if (stopTimer == false)
            {
                youWin.SetActive(true);

                if (nameScene == "Level1")
                {
                    btnNextLevel.SetActive(true);
                    bestRecord = 1;
                    PlayerPrefs.SetInt("BestRecord",bestRecord);
                }
                else if (nameScene == "Level2")
                {
                    btnFinish.SetActive(true);
                    bestRecord = 2;
                    PlayerPrefs.SetInt("BestRecord",bestRecord);
                }
            }
            
            stopTimer = true;
            panelResult.SetActive(true);
            txtCountTrue.text = countTrue.ToString();
            txtCountFalse.text = countFalse.ToString();
            txtTotalTimer.text = timer.ToString("00");
        }

        public void BtnNext()
        {
            SceneManager.LoadScene("Level2");
            if (PlayerPrefs.GetInt("CountAudio") == 1)
            {
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
            }
            else if (PlayerPrefs.GetInt("CountAudio") == 0)
            {
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = false;
            }
        }
        public void BtnFinish()
        {
            SceneManager.LoadScene("Menu");
            if (PlayerPrefs.GetInt("CountAudio") == 1)
            {
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
            }
            else if (PlayerPrefs.GetInt("CountAudio") == 0)
            {
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = false;
            }
        }

        public void BtnPause()
        {
            GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
            Time.timeScale = 0;
            panelPause.SetActive(true);
        }

        public void BtnResume()
        {
            if (PlayerPrefs.GetInt("CountAudio") == 1)
            {
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
            }
            else if (PlayerPrefs.GetInt("CountAudio") == 0)
            {
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = false;
            }
            Time.timeScale = 1;
            panelPause.SetActive(false);
        }

        public void Character(int count)
        {
            switch (count)
            {
                case 0:
                    imgCharacter.sprite = spNormal;
                    break;
                case 1:
                    imgCharacter.sprite = spHappy;
                    break;
                case 2:
                    imgCharacter.sprite = spSad;
                    break;
            }
        }
        
        public void BtnQuit()
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
        }

        public void BtnAgain()
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
