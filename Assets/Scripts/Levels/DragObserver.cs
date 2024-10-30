using Connection;
using Events;
using Player.ActionHandlers;
using UnityEngine;

namespace Levels
{
    public class DragObserver : MonoBehaviour
    {
        [SerializeField] private ColorConnectionManager colorConnectionManager;
        
        private ClickHandler _clickHandler;
        private ColorNode _node;

        private void Awake()
        {
            _clickHandler = ClickHandler.Instance;
            
            _clickHandler.DragStartEvent += OnDragStart;
            _clickHandler.DragEndEvent += OnDragEnd;
        }

        private void OnDestroy()
        {
            _clickHandler.DragStartEvent -= OnDragStart;
            _clickHandler.DragEndEvent -= OnDragEnd;
        }
        
        private void OnDragStart(Vector3 position)
        {
            colorConnectionManager.TryGetColorNodeInPosition(position, out var node);
            
            _node = node;
            
            if (node == null)
                EventsController.Fire(new EventModels.Game.ScrollStarted());
        }
        
        private void OnDragEnd(Vector3 position)
        {
            if (_node == null)
                EventsController.Fire(new EventModels.Game.ScrollFinished());
        }
    }
}