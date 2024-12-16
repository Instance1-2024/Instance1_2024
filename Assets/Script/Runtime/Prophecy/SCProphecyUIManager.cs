using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Runtime.Prophecy
{
    public class SCProphecyUIManager : MonoBehaviour
    {

        public static SCProphecyUIManager Instance;

        public UnityEvent<int,bool> GetMemoryEvent;

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
            
            GetMemoryEvent = new UnityEvent<int,bool>();
            GetMemoryEvent.AddListener(Test);

            if(SCProphecyManager.Instance != null)
                SCProphecyManager.Instance.UpdateProphecyUiManager();
        }

        private void Update()
        {
            if (_id > 0 && _hasPlayed && !_propheciesGO[_id - 1].GetComponent<Animation>().IsPlaying(
                    _propheciesGO[_id - 1].GetComponent<Animation>().clip.name))
            {
                _time += Time.deltaTime;
                if (_time >= _timer)
                {
                    if (SCProphecyManager.Instance != null)
                        foreach (int memoryID in SCProphecyManager.Instance.GetMemoryIds())
                        {
                            _propheciesGO[memoryID - 1].SetActive(false);
                        }
                    _hasPlayed = false;
                    _time = 0f;
                }
            }
        }

        private void Test(int i, bool fromSave = false)
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

            // if the id is issued from the level (not from the save) we do the base animation & show all current other memory pieces
            if (!fromSave)
            {
                if (SCProphecyManager.Instance != null)
                    foreach (int memoryID in SCProphecyManager.Instance.GetMemoryIds())
                    {
                        _propheciesGO[memoryID - 1].SetActive(true);
                    }
                _propheciesGO[i - 1].SetActive(true);
                _propheciesGO[i - 1].GetComponent<Animation>().Play(_propheciesGO[i - 1].GetComponent<Animation>().clip.name);
            }
            else // if the id is issued from the save (previous levels), we set all memory pieces to their last animation frame (final position). It's called on this script start.
            {
                if (SCProphecyManager.Instance != null)
                    foreach (int memoryID in SCProphecyManager.Instance.GetMemoryIds())
                    {
                        AnimationClip clip = _propheciesGO[memoryID - 1].GetComponent<Animation>().GetClip(_propheciesGO[memoryID - 1].GetComponent<Animation>().clip.name);

                        if (clip)
                        {
                            _propheciesGO[memoryID - 1].GetComponent<Animation>()[_propheciesGO[memoryID - 1].GetComponent<Animation>().clip.name].time = _propheciesGO[memoryID - 1].GetComponent<Animation>().clip.length;
                            _propheciesGO[memoryID - 1].GetComponent<Animation>().Play(_propheciesGO[memoryID - 1].GetComponent<Animation>().clip.name);
                        }
                    }
            }

            // Update save in case an id is missing
            if (SCProphecyManager.Instance != null)
                SCProphecyManager.Instance.AddMemoryPiece(_id);
        }
        
        
        
    }
}
