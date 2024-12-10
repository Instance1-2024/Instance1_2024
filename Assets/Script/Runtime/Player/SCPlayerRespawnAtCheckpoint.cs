using Script.Runtime.ColorManagement;
using UnityEngine;

namespace Script.Runtime.Player
{
    public class SCPlayerRespawnAtCheckpoint : MonoBehaviour
    {
        private Transform _transform;

        [SerializeField] private PlayerDataRespawn _playerDataRespawn;

        private SCPlayerChangeColor _changeColor;
        private void Start()
        {
            _transform = transform;
            SCCheckpointManager.Instance.ReachCheckpoint.AddListener(OnCheckPoint);
            _changeColor = GetComponent<SCPlayerChangeColor>();
            _playerDataRespawn = new PlayerDataRespawn
            {
                LastCheckpoint = _transform.position,
                Rotation = _transform.rotation,
                Color = _changeColor.GetColor(),
                IsHandle = false
            };
        }

        /// <summary>
        /// Get the value to the player when entering a checkpoint.
        /// </summary>
        private void OnCheckPoint()
        {
            _playerDataRespawn.LastCheckpoint = _transform.position;
            _playerDataRespawn.Rotation = _transform.rotation;
            _playerDataRespawn.Color = _changeColor.GetColor();
            // set the Handle 
        }
        
        /// <summary>
        /// Set the value to the player when respawning.
        /// </summary>
        [ContextMenu("TestRespawn")]
        public void OnRespawn()
        {
            _transform.position = _playerDataRespawn.LastCheckpoint;
            _transform.rotation = _playerDataRespawn.Rotation;
            _changeColor.ChangeColor(_playerDataRespawn.Color);
            //Set the Handle
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