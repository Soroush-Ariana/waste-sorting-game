using UnityEngine;

namespace _MyFiles._Scripts
{
    public class BGMusic : MonoBehaviour
    {
        #region Val
        
        public static AudioSource audioSource;
    
        private static BGMusic instance = null;

        #endregion


        #region Func

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();

                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        #endregion
    }
}
