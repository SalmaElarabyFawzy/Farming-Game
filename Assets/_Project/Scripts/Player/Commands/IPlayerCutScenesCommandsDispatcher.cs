using Assets.Scripts.Interfaces;
using Farm.Inventory.Presenter;
using Farm.Player.View;

namespace Farm.Player.Commands
{
    public interface IPlayerCutScenesCommandsDispatcher
    {
        bool TryPlayCutScene(IInteractable interactable);
    }
}