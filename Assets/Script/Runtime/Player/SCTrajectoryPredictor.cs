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
        float _increment = 0.025f;
        
        [SerializeField, Range(1.05f, 2f), Tooltip("The raycast overlap between points in the trajectory, this is a multiplier of the length between points. 2 = twice as long")]
        float _rayOverlap = 1.1f;
        
        [SerializeField] LayerMask _ignoreBallLayers;
        [SerializeField] private LayerMask _ignoreWallLayers;
        
        private SCPlayerHold _playerHold;

        [SerializeField] private List<LayerMask> _ignoredLayer;
        
        private void Start() {
            _line = GetComponent<LineRenderer>();
            _playerHold = GetComponent<SCPlayerHold>();
        }

        public void PredictTrajectory(SProjectileData projectile) {
            Vector3 velocity = projectile.initSpeed / projectile.mass * projectile.direction;
            Vector3 position = projectile.initPos;
            Vector3 nextPosition;
            float overlap;

            _ignoreWallLayers = GetWallLayer();
            
            for (int u = 0; u < _maxPoints; u++) {
                
                velocity = CalculateNewVelocity(velocity, projectile.drag, _increment);
                nextPosition = position + velocity * _increment;
                
                overlap = Vector3.Distance(position, nextPosition) * _rayOverlap;

                if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap, ~(_ignoreBallLayers |  _ignoreWallLayers))) {

                    UpdateLineRender(u,(u-1, hit.point));
                    break;
                }

                position = nextPosition;
                UpdateLineRender(_maxPoints, (u, position));
                
            }
        }
        
        Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment) {
            velocity += Physics.gravity * increment;
            velocity *= Mathf.Clamp01(1f - drag * increment);
            return velocity;
        }
        
        void UpdateLineRender(int count, (int point, Vector3 pos) pointPos) {
            if(count <= 0)return;
            _line.positionCount = count;
            _line.SetPosition(pointPos.point, pointPos.pos);            
        }
        
        LayerMask GetWallLayer() {
            if (!_playerHold.IsHolding) return 0;
            if (_playerHold.HoldItem.TryGetComponent(out SCColorChange colorChange)) {
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
        
        public void SetTrajectoryVisible (bool visible) {
            _line.enabled = visible;
        }

    }
    
    public struct SProjectileData {
        public Vector3 direction;
        public Vector3 initPos;
        public float initSpeed;
        public float mass;
        public float drag;
    }
}
