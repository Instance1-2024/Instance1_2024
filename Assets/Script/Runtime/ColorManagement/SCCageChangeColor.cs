using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static Script.Runtime.SCEnum;
using static UnityEngine.Random;

namespace Script.Runtime.ColorManagement {
    public class SCCageChangeColor : SCChangeColor {
        [SerializeField] private MeshRenderer _monsterRenderer;

        [SerializeField] private List<Material> _blackMonsters;
        [SerializeField] private List<Material> _whiteMonsters;
        
        public override void ChangeColor(EColor color) {
            switch (color) {
                case EColor.White:
                    ApplyColor(_white);
                    ExcludeLayer(_black.Layer);
                    ApplyMonsters(_whiteMonsters);
                    break;
                case EColor.Black:
                    ApplyColor(_black);
                    ExcludeLayer(_white.Layer);
                    ApplyMonsters(_blackMonsters);
                    break;
                case EColor.Gray:
                    ApplyColor(_gray);
                    List<Material> _monsters = new(_blackMonsters);
                    _monsters.AddRange(_whiteMonsters);
                    ApplyMonsters(_monsters);
                    break;
            }

            CurrentColor = color;
        }

        private void ApplyMonsters(List<Material> monsters) {
            if (monsters.Count > 0) {
                int randomIndex = Range(0, monsters.Count);
                _monsterRenderer.material = monsters[randomIndex];
            }
        }
    }
}