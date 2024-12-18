using System;
using Script.Runtime.ColorManagement;
using System.Collections;
using Script.Runtime.InputSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Runtime.Player
{
    public class SCPlayerRespawnAtCheckpoint : MonoBehaviour {
        private Transform _transform;
        private SCPlayerMovement _playerMovement;
        SCInputManager _inputManager => SCInputManager.Instance;

        [SerializeField] private PlayerDataRespawn _playerDataRespawn;

        private ScPlayerChangeColor _color;
        private SCSanity _sanity;
        private bool _isRespawning = false;

        [Header("Death Animation")]
        [SerializeField] private ParticleSystem _particleSystemWhite;
        [SerializeField] private ParticleSystem _particleSystemBlack;
        private void Start()
        {
            _playerMovement = GetComponent<SCPlayerMovement>();
            _transform = transform;
            SCCheckpointManager.Instance.ReachCheckpoint.AddListener(OnCheckPoint);
            _color = GetComponent<ScPlayerChangeColor>();
            _sanity = GetComponent<SCSanity>();
            _playerDataRespawn = new PlayerDataRespawn
            {
                LastCheckpoint = _transform.position,
                Rotation = _transform.rotation,
                Color = _color.GetColor(),
                IsHandle = false
            };
        }

        private void Update() {
            _inputManager.IsInputActive =!_isRespawning;
            _playerMovement.SetVelocityLock(_isRespawning);
            
        }

        /// <summary>
        /// Get the value to the player when entering a checkpoint.
        /// </summary>
        private void OnCheckPoint()
        {
            _playerDataRespawn.LastCheckpoint = _transform.position;
            _playerDataRespawn.Rotation = _transform.rotation;
            _playerDataRespawn.Color = _color.GetColor();
            // set the Handle 
        }
        
        /// <summary>
        /// Set the value to the player when respawning.
        /// </summary>
        [ContextMenu("TestRespawn")]
        public void OnRespawn()
        {
            if(_isRespawning) return;
            StartCoroutine(RespawnPlayer());
        }

        private IEnumerator RespawnPlayer ()
        {
            _isRespawning = true;
            switch (_color.GetColor())
            {
                case SCEnum.EColor.White:
                    if (!_particleSystemWhite)
                    {
                        StopAllCoroutines();
                    }
                    _particleSystemWhite.gameObject.SetActive(true);
                    _particleSystemWhite.Play();



                    while (_transform.GetChild(0).GetChild(0).GetChild(0).localScale.x > 0)
                    {
                        _transform.GetChild(0).GetChild(0).GetChild(0).localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
                        yield return null;
                    }
                    yield return new WaitForSeconds(1);
                    break;
                case SCEnum.EColor.Black:
                    if (!_particleSystemBlack)
                    {
                        StopAllCoroutines();
                    }
                    _particleSystemBlack.gameObject.SetActive(true);
                    _particleSystemBlack.Play();



                    while (_transform.GetChild(0).GetChild(1).GetChild(0).localScale.x > 0)
                    {
                        _transform.GetChild(0).GetChild(1).GetChild(0).localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
                        yield return null;
                    }
                    yield return new WaitForSeconds(1);
                    break;
            }

            yield return new WaitForSeconds(1);
            _transform.position = _playerDataRespawn.LastCheckpoint;
            _transform.rotation = _playerDataRespawn.Rotation;
            _color.ChangeColor(_playerDataRespawn.Color);
            _sanity.UpdateSanity(10f);
            //Set the Handle

            // reset everything so next death everything is working
            _transform.GetChild(0).GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 1);
            _transform.GetChild(0).GetChild(1).GetChild(0).localScale = new Vector3(1, 1, 1);
            _particleSystemWhite.gameObject.SetActive(false);
            _particleSystemBlack.gameObject.SetActive(false);


            _isRespawning = false;

            yield return null;
        }
    }


    [System.Serializable]
    public struct PlayerDataRespawn
    {
        public Vector3 LastCheckpoint;
        public Quaternion Rotation;
        public SCEnum.EColor Color;
        public bool IsHandle;
    }
}