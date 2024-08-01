using UnityEngine;
using UnityEngine.Playables;

public class SkipCutscene : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skip();
        }
    }

    void Skip()
    {
        playableDirector.time = playableDirector.duration;
        playableDirector.Evaluate();
    }
}
