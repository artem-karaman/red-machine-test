using System;
using Events;


namespace Player
{
    public class PlayerSateObserver
    {
        private readonly Action<PlayerState> _setStateAction;
        
        
        public PlayerSateObserver(Action<PlayerState> setStateAction)
        {
            _setStateAction = setStateAction;
        }

        public void Subscribe()
        {
            EventsController.Subscribe<EventModels.Game.NodeTapped>(this, OnNodeTapped);
            EventsController.Subscribe<EventModels.Game.PlayerFingerRemoved>(this, OnPlayerFingerRemoved);
            EventsController.Subscribe<EventModels.Game.ScrollStarted>(this, OnScrollStarted);
            EventsController.Subscribe<EventModels.Game.ScrollFinished>(this, OnScrollFinished);
        }

        public void Unsubscribe()
        {
            EventsController.Unsubscribe<EventModels.Game.NodeTapped>(OnNodeTapped);
            EventsController.Unsubscribe<EventModels.Game.PlayerFingerRemoved>(OnPlayerFingerRemoved);
            EventsController.Unsubscribe<EventModels.Game.ScrollStarted>(OnScrollStarted);
            EventsController.Unsubscribe<EventModels.Game.ScrollFinished>(OnScrollFinished);
        }

        private void OnNodeTapped(EventModels.Game.NodeTapped e)
        {
            _setStateAction?.Invoke(PlayerState.Connecting);
        }

        private void OnPlayerFingerRemoved(EventModels.Game.PlayerFingerRemoved e)
        {
            _setStateAction?.Invoke(PlayerState.None);
        }

        private void OnScrollStarted(EventModels.Game.ScrollStarted e)
        {
            _setStateAction?.Invoke(PlayerState.Scrolling);
        }

        private void OnScrollFinished(EventModels.Game.ScrollFinished obj)
        {
            _setStateAction?.Invoke(PlayerState.None);
        }
    }
}
