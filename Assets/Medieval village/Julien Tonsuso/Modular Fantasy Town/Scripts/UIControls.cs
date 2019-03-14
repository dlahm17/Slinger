using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace julientonsuso.common
{
    public class UIControls : MonoBehaviour
    {

        public Button btnLeft;
        public Button btnRight;

        // Use this for initialization
        void Start()
        {

        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Shop()
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/90075");
        }


        public void App()
        {
            Application.OpenURL("https://drive.google.com/file/d/0B26awG-KMeldcWlIdzROZnhYZUU/view?usp=sharing");
        }


        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                btnRight.onClick.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                btnLeft.onClick.Invoke();
            }

        }
    }
}
