using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;
    private PortalTriggerInteraction.PortalToSpawnAt _portalToSpawnTo;
    private static bool _loadedFromPortal;
    private RoomFirstDungeonGenerator _roomFirstDungeonGenerator;
    private GameObject _player;
    private Collider2D _playerCollider;
    private Collider2D _portalCollider;
    private Vector3 _playerSpawnPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void SwapSceneFromPortalUse(SceneField myScene, PortalTriggerInteraction.PortalToSpawnAt portalToSpawnAt)
    {
        _loadedFromPortal = true;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene, portalToSpawnAt));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene, PortalTriggerInteraction.PortalToSpawnAt portalToSpawnAt = PortalTriggerInteraction.PortalToSpawnAt.None)
    {
        SceneFadeManager.instance.StartFadeOut();
        while (SceneFadeManager.instance.IsFadingOut)
        {
            yield return null;
        }

        _portalToSpawnTo = portalToSpawnAt;
        SceneManager.LoadScene(myScene);
        //if(myScene.SceneName == "Blue Dungeon")
        //{
        //    _roomFirstDungeonGenerator.GenerateDungeon();
        //    Debug.Log(myScene.SceneName);
        //}
        
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneFadeManager.instance.StartFadeIn();

        // Gọi hàm GenerateDungeon nếu scene là "Blue Dungeon"
        if (scene.name == "Blue Dungeon")
        {
            _roomFirstDungeonGenerator = FindObjectOfType<RoomFirstDungeonGenerator>();
            if (_roomFirstDungeonGenerator != null)
            {
                _roomFirstDungeonGenerator.GenerateDungeon();
            }
            else
            {
                Debug.LogWarning("RoomFirstDungeonGenerator not found in the scene.");
            }
        }

        if (_loadedFromPortal)
        {
            FindPortal(_portalToSpawnTo);
            _player.transform.position = _playerSpawnPosition;
            _loadedFromPortal = false;
        }
    }


    private void FindPortal(PortalTriggerInteraction.PortalToSpawnAt portalSpawnNumber)
    {
        PortalTriggerInteraction[] portals = FindObjectsOfType<PortalTriggerInteraction>();
        for(int i = 0; i < portals.Length; i++)
        {
            if (portals[i].CurrentPortalPosition == portalSpawnNumber)
            {
                _portalCollider = portals[i].gameObject.GetComponent<Collider2D>();
                CalculateSpawnPosition();

                return;
            }
        }
    }
    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerCollider.bounds.extents.y;
        _playerSpawnPosition = _portalCollider.transform.position - new Vector3(0f,colliderHeight, 0f);
    }
}
