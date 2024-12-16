using UnityEngine;

namespace Script.Runtime.LevelObject.Pebble {
    public interface IThrowable {
        public LayerMask ThrowBlack { get; set; }
        public LayerMask ThrowGray { get; set; }
        public LayerMask ThrowWhite { get; set; }

        public void RemoveCollisions();
        public void GiveCollisions();
    }
}