

using Assets.Scripts.Interfaces;

namespace Farm.Player.Commands
{
    public interface IPlayerCutSceneCommand
    {
        bool TryExecuteCutScene(IInteractable interactable);
    }
}