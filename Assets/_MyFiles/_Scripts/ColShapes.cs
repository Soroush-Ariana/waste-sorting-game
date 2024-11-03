using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _MyFiles._Scripts
{
    public class ColShapes : MonoBehaviour
    {
        #region Val

        public float z = 10f;
        
        public string name;

        public GameObject parentGo;

        private Image sP;

        #endregion


        
        #region Func

        private void Start()
        {
            sP = GetComponent<Image>();
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("_Menu");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == name)
            {
                GameObject.Find("Audio Correct").GetComponent<AudioSource>().Play();
                FindObjectOfType<GameManager>().Character(1);
                Invoke(nameof(CharacterNormal), 1.5f);
                
                GetComponent<BoxCollider2D>().enabled = false;
                
                FindObjectOfType<GameManager>().countTrue += 1;
                FindObjectOfType<GameManager>().countAll += 1;

                Destroy(gameObject);

                if (FindObjectOfType<GameManager>().countAll >= 9)
                {
                    FindObjectOfType<GameManager>().ShowResult();
                }
            }
            else
            {
                GameObject.Find("Audio Wrong").GetComponent<AudioSource>().Play();
                FindObjectOfType<GameManager>().Character(2);
                Invoke(nameof(CharacterNormal), 1.5f);
                
                FindObjectOfType<GameManager>().countFalse += 1;
            }
        }

        private void CharacterNormal()
        {
            FindObjectOfType<GameManager>().Character(0);
        }
        
        private void OnMouseDrag()
        {
            gameObject.transform.parent = parentGo.transform;

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objPos;
        }

        #endregion
    }
}
