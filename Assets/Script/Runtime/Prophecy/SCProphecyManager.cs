using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Prophecy
{
    public class SCProphecyManager : MonoBehaviour
    {

        public static SCProphecyManager Instance;

        public UnityEvent<int> GetMemoryEvent;

        [SerializeField] private List<GameObject> _propheciesGO = new List<GameObject>();
        private List<int> _memoryIDs;

        [SerializeField] private float _timer = 5f;
        private float _time = 0f;
        
        private bool _hasPlayed = false;

        private int _id;
        
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
            foreach (GameObject g in _propheciesGO)
            {
                g.gameObject.SetActive(false);
            }
            _memoryIDs = new List<int>();
            
            GetMemoryEvent = new UnityEvent<int>();
            GetMemoryEvent.AddListener(Test);
        }

        private void Update()
        {
            if (_id > 0 && _hasPlayed && !_propheciesGO[_id - 1].GetComponent<Animation>().IsPlaying(
                    _propheciesGO[_id - 1].GetComponent<Animation>().clip.name))
            {
                _memoryIDs.Add(_id);

                _time += Time.deltaTime;
                if (_time >= _timer)
                {
                    foreach (int memoryID in _memoryIDs)
                    {
                        _propheciesGO[memoryID - 1].SetActive(false);
                    }
                    _hasPlayed = false;
                    _time = 0f;
                }
            }
        }

        private void Test(int i)
        {
            _id = i;
            if (_memoryIDs.Count > 0)
            {
                foreach (int memoryID in _memoryIDs)
                {
                    _propheciesGO[memoryID - 1].gameObject.SetActive(true);
                }
            }
            
            _hasPlayed = true;
            _propheciesGO[i - 1].SetActive(true);
            _propheciesGO[i - 1].GetComponent<Animation>().Play(_propheciesGO[i - 1].GetComponent<Animation>().clip.name);
            
        }
        
        
        
    }
}
