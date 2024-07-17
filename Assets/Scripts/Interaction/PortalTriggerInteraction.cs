using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTriggerInteraction : TriggerInteractionBase
{
    public enum PortalToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four,
    }
    [Header("Spawn To")]
    [SerializeField] private PortalToSpawnAt portalToSpawnAt;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("THIS Door")]
    public PortalToSpawnAt CurrentPortalPosition;
    public override void Interact()
    {
        //load new scene
        SceneSwapManager.SwapSceneFromPortalUse(_sceneToLoad, portalToSpawnAt);
    }
}
