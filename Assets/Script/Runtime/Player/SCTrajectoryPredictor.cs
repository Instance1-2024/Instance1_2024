using System;
using System.Collections.Generic;
using Script.Runtime.ColorManagement;
using UnityEngine;
using static Script.Runtime.SCEnum;

namespace Script.Runtime.Player {
    public class SCTrajectoryPredictor : MonoBehaviour {
        private LineRenderer _line;
        
        [SerializeField,Range(10, 100), Tooltip("The maximum number of points the LineRenderer can have")] 
        private int _maxPoints;
        
        [SerializeField, Range(0.01f, 0.5f), Tooltip("The time increment used to calculate the trajectory")]
        float _increment;
        
        [SerializeField, Range(1.05f, 2f), Tooltip("The raycast overlap between points in the trajectory, this is a multiplier of the length between points. 2 = twice as long")]
        float _rayOverlap;
        
        [SerializeField] LayerMask _ignoreBallLayers;
        [SerializeField] private LayerMask _ignoreWallLayers;
        
        private SCPlayerHold _playerHold;

        [SerializeField] private List<LayerMask> _ignoredLayer;
        
        private void Start() {
            _line = GetComponent<LineRenderer>();
            _playerHold = GetComponent<SCPlayerHold>();
        }

        /// <summary>
        /// Predict the trajectory of the projectile
        /// </summary>
        /// <param name="projectile">Projectile data</param>
        public void PredictTrajectory(SProjectileData projectile) {
            Vector3 velocity = projectile.InitSpeed / projectile.Mass * projectile.Direction;
            Vector3 position = projectile.InitPos;

            _ignoreWallLayers = GetWallLayer();
            
            for (int u = 0; u < _maxPoints; u++) {
                
                velocity = CalculateNewVelocity(velocity, projectile.Drag, _increment);
                Vector3 nextPosition = position + velocity * _increment;
                
                float overlap = Vector3.Distance(position, nextPosition) * _rayOverlap;

                if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap, ~(_ignoreBallLayers |  _ignoreWallLayers))) {

                    UpdateLineRender(u,(u-1, hit.point));
                    break;
                }

                position = nextPosition;
                UpdateLineRender(_maxPoints, (u, position));
                
            }
        }
        
        /// <summary>
        /// Calculate the new velocity with the gravity
        /// </summary>
        /// <param name="velocity">Old velocity of the projectile</param>
        /// <param name="drag"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment) {
            velocity += Physics.gravity * increment;
            velocity *= Mathf.Clamp01(1f - drag * increment);
            return velocity;
        }
        
        /// <summary>
        /// Update the LineRenderer for this point
        /// </summary>
        /// <param name="count">number of points</param>
        /// <param name="pointPos.point">index of the point</param>
        /// <param name="pointPos.pos">position of the point</param>
        void UpdateLineRender(int count, (int point, Vector3 pos) pointPos) {
            if(count <= 0)return;
            _line.positionCount = count;
            _line.SetPosition(pointPos.point, pointPos.pos);            
        }
        
        /// <summary>
        /// Get the wall layer depending on the color of the held item
        /// </summary>
        /// <returns>The wall layer</returns>
        LayerMask GetWallLayer() {
            if (!_playerHold.IsHolding) return 0;
            if (_playerHold.HoldItem.TryGetComponent(out SCChangeColor colorChange)) {
                switch (colorChange.GetColor()) {
                    case EColor.Black:
                        return _ignoredLayer[0];
                    case EColor.White:
                        return _ignoredLayer[1];
                    case EColor.Gray:
                        return 0;
                }
            }

            return 0;
        }
        
        /// <summary>
        /// Show the trajectory or not
        /// </summary>
        /// <param name="visible"></param>
        public void SetTrajectoryVisible (bool visible) {
            _line.enabled = visible;
        }

    }
    
    /// <summary>
    /// Info of the projectile to calculate the trajectory
    /// </summary>
    /// <param name="direction">Direction of the projectile</param>
    /// <param name="initPos">Initial position of the projectile</param>
    /// <param name="initSpeed">Initial speed of the projectile</param>
    /// <param name="mass">Mass of the projectile</param>
    /// <param name="drag"></param>
    public struct SProjectileData {
        public Vector3 Direction;
        public Vector3 InitPos;
        public float InitSpeed;
        public float Mass;
        public float Drag;
    }
}
