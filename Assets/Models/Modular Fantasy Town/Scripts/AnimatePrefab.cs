using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace julientonsuso.common
{
    public class AnimatePrefab : MonoBehaviour
    {

        public float TimeScale = 2f;
        public List<GameObject> targets = new List<GameObject>();
        private int index = 0;
        private GameObject target;
        Dictionary<GameObject, Vector3> list = new Dictionary<GameObject, Vector3>();

        // Use this for initialization
        void Start()
        {

            Next();

        }

        public void Previous()
        {
            index--;
            if (index < 0) index = targets.Count - 1;

            Reset();
            foreach (GameObject go in targets) go.SetActive(false);

            target = targets[index];
            target.SetActive(true);

            Hide();
        }

        public void Next()
        {
            index++;
            if (index >= targets.Count) index = 0;

            Reset();
            foreach (GameObject go in targets) go.SetActive(false);

            target = targets[index];
            target.SetActive(true);



            Hide();
        }


        // Update is called once per frame
        void Update()
        {

        }


        private void GetChildRecursive(GameObject obj)
        {
            if (null == obj)
                return;

            foreach (Transform child in obj.transform)
            {
                if (null == child)
                    continue;
                //child.gameobject contains the current child you can do whatever you want like add it to an array

                GameObject go = child.gameObject;
                list.Add(go, go.transform.localScale);


                GetChildRecursive(child.gameObject);
            }
        }

        void Hide()
        {

            GetChildRecursive(target);

            foreach (GameObject go in list.Keys)
            {
                go.transform.localScale = new Vector3();
            }
            /*foreach (Transform child in target.transform)
            {
                GameObject go = child.gameObject;
                list.Add(go, go.transform.localScale);
                go.transform.localScale = new Vector3();
            }*/

            Show();
            //Invoke("Show", 0.5f);


        }


        void Show()
        {
            float delta = 0f;
            foreach (GameObject go in list.Keys)
            {
                StartCoroutine(LerpUp(go, delta));
                delta += Random.Range(0.02f, 0.15f);
            }
        }

        void Reset()
        {
            StopAllCoroutines();

            foreach (GameObject go in list.Keys)
            {
                go.transform.localScale = list[go];
            }

            list.Clear();
        }


        IEnumerator LerpUp(GameObject go, float delta)
        {
            yield return new WaitForSeconds(delta/*Random.Range(0f,6f)*/);
            float progress = 0;
            Vector3 FinalScale = list[go];
            Vector3 InitialScale = new Vector3();
            // float TimeScale = 2f;

            while (progress <= 1)
            {
                go.transform.localScale = Vector3.Lerp(InitialScale, FinalScale, progress);
                progress += Time.deltaTime * TimeScale;
                yield return null;
            }

            go.transform.localScale = FinalScale;

        }



    }
}
