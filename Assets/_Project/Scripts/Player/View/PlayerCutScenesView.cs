

using Farm.Core.DesignPatterns.EventSystem;
using Farm.Player.Events;
using UnityEngine;
using UnityEngine.Playables;
using VContainer;

namespace Farm.Player.View
{

    public class PlayerCutScenesView : MonoBehaviour
    {
        [Header("Cutscene")]
        [SerializeField] private PlayableDirector director;
        [SerializeField] private PlayableAsset plowCutscene;
        [SerializeField] private PlayableAsset waterCutscene;
        [SerializeField] private PlayableAsset harvestCutscene;
        [SerializeField] private PlayableAsset plantCutscene;

        private EventSystem _eventSystem;

        [Inject]
        public void Construct(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public void PlayPlowCutScene()
        {
            director.Play(plowCutscene);
        }
        public void PlayWaterCutScene()
        {
            director.Play(waterCutscene);
        }
        public void PlayHarvestCutScene()
        {
            director.Play(harvestCutscene);
        }
        public void PlayPlantCutScene()
        {
            director.Play(plantCutscene);
        }

        public void StartCutScene()
        {
            _eventSystem.Publish(new PlayerCutSceneStartedEvent());
        }

        public void FinishCutScene()
        {
            _eventSystem.Publish(new PlayerCutSceneFinishedEvent());
        }
    }
}