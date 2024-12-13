using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Prophecy
{
    public class SCProphecyManager : MonoBehaviour
    {

        public static SCProphecyManager Instance;

        public UnityEvent<int> GetMemoryEvent;

        [SerializeField] private List<Transform> _propheciesTransform = new List<Transform>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            GetMemoryEvent = new UnityEvent<int>();
            GetMemoryEvent.AddListener(Test);
        }

        private void Test(int i)
        {
            // switch (i)
            // {
            //     case 1 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            //     case 2 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            //     case 3 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            //     case 4 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            //     case 5 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            //     case 6 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            //     case 7 :
            //         _propheciesTransform[0].GetComponent<Animation>().Play();
            //         break;
            // }

            _propheciesTransform[i - 1].GetComponent<Animation>().Play();
        }
        
    }
}
