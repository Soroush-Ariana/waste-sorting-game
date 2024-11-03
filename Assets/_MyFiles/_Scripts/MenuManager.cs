using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _MyFiles._Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject panelSetting;
        public GameObject panelAbout;
        public GameObject panelHowToPlay;

        public Image imgAudio;
        public Sprite spOn, spOff;
        public int countAudio;

        public int bestRecord;
        public Text txtBestRecord;


       

        private void Start()
        {
            //PlayerPrefs.DeleteAll();

            if (PlayerPrefs.HasKey("CountAudio"))
            {
                countAudio = PlayerPrefs.GetInt("CountAudio");
                if (countAudio == 0)
                {
                    imgAudio.sprite = spOn;
                    GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = false;
                }
                else if (countAudio == 1)
                {
                    imgAudio.sprite = spOff;
                    GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
                }
            }

            if (PlayerPrefs.HasKey("BestRecord"))
            {
                bestRecord = PlayerPrefs.GetInt("BestRecord");
                txtBestRecord.text = bestRecord.ToString();
            }
        }

        public void BtnPlay()
        {
            SceneManager.LoadScene("Level1");
        }

        public void BtnSetting()
        {
            panelSetting.SetActive(true);
        }
        public void BtnCloseSetting()
        {
            panelSetting.SetActive(false);
        }
        public void BtnAbout()
        {
            panelAbout.SetActive(true);
        }
        public void BtnCloseAbout()
        {
            panelAbout.SetActive(false);
        }
        public void BtnHowToPlay()
        {
            panelHowToPlay.SetActive(true);
        }
        public void BtnCloseHowToPlay()
        {
            panelHowToPlay.SetActive(false);
        }

        public void BtnAudio()
        {
            if (countAudio == 0)
            {
                countAudio = 1;
                
                imgAudio.sprite = spOff;
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = true;
                PlayerPrefs.SetInt("CountAudio", countAudio);
            }
            else if (countAudio == 1)
            {
                countAudio = 0;
                
                imgAudio.sprite = spOn;
                GameObject.Find("Audio_BG").GetComponent<AudioSource>().mute = false;
                PlayerPrefs.SetInt("CountAudio", countAudio);
            }
        }

        public void BtnQuit()
        {
            Application.Quit();
        }
    }
}
