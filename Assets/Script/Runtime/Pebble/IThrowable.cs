using UnityEngine;

namespace Script.Runtime.Pebble {
    public interface IThrowable {
        public LayerMask ThrowBlack { get; set; }
        public LayerMask ThrowGray { get; set; }
        public LayerMask ThrowWhite { get; set; }

        public void Take();
        public void Reset();
    }
}