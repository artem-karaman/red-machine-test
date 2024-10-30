using System.Collections;
using Player;
using Player.ActionHandlers;
using UnityEngine;
using Utils.MonoBehaviourUtils;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private CameraSettings cameraSettings;
        
        private ClickHandler _clickHandler;
        private Vector3 _startDragPosition;
        private Vector3 _endDragPosition;
        private Transform _cameraTransform;
        
        private readonly Vector3 _cameraDefaultPosition = new(0f, 0f, -10f);

        private void Awake()
        {
            _clickHandler = ClickHandler.Instance;
            _cameraTransform = CameraHolder.Instance.transform;
            
            _clickHandler.DragStartEvent += OnDragStart;
            _clickHandler.DragEndEvent += OnDragEnd;
        }

        private void OnDestroy()
        {
            if (_cameraTransform != null)
            {
                _cameraTransform.position = _cameraDefaultPosition;
            }
            
            _clickHandler.DragStartEvent -= OnDragStart;
            _clickHandler.DragEndEvent -= OnDragEnd;
        }

        private void OnDragStart(Vector3 startVector)
        {
            _startDragPosition = startVector;
        }

        private void OnDragEnd(Vector3 endVector)
        {
            if (PlayerController.PlayerState != PlayerState.Scrolling)
                return;
            
            _endDragPosition = endVector;
            
            Coroutines.Instance.StartCoroutine(
                MoveCameraRoutine(_cameraTransform.position, CameraTransformEndPosition()));
        }

        private Vector3 CameraTransformEndPosition()
        {
            var endPosition = _cameraTransform.position - (_endDragPosition - _startDragPosition);
            return new (
                Mathf.Clamp(
                    endPosition.x, 
                    cameraSettings.CameraMinPositionToScroll.x,
                    cameraSettings.CameraMaxPositionToScroll.x),
                Mathf.Clamp(
                    endPosition.y, 
                    cameraSettings.CameraMinPositionToScroll.y,
                    cameraSettings.CameraMaxPositionToScroll.y));
        }

        IEnumerator MoveCameraRoutine(Vector3 startPosition, Vector3 endPosition)
        {
            float elapsedTime = 0;

            while (elapsedTime < cameraSettings.CameraScrollAnimationDuration)
            {
                _cameraTransform.position = Vector3.Lerp(
                    new Vector3(startPosition.x, startPosition.y, _cameraDefaultPosition.z), 
                    new Vector3(endPosition.x, endPosition.y, _cameraDefaultPosition.z), 
                    elapsedTime / cameraSettings.CameraScrollAnimationDuration);
                
                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }
            _cameraTransform.position = new (endPosition.x, endPosition.y, _cameraDefaultPosition.z);
        }
    }
}
