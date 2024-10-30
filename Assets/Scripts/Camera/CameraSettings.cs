using UnityEngine;

namespace Camera
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObjects/CameraSettings", order = 1)]
    public class CameraSettings : ScriptableObject
    {
        [SerializeField]
        [Range(4.0f, 5.0f)]
        private float cameraScrollAnimationDuration;
        
        [SerializeField]
        private Vector3 cameraMinPositionToScroll;
        
        [SerializeField]
        private Vector3 cameraMaxPositionToScroll;
        
        public float CameraScrollAnimationDuration => cameraScrollAnimationDuration;
        public Vector3 CameraMinPositionToScroll => cameraMinPositionToScroll;
        public Vector3 CameraMaxPositionToScroll => cameraMaxPositionToScroll;
    }
}