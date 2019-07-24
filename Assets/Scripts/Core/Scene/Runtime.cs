using UnityEngine;
namespace Core.Scene
{
    public class Runtime {
    private const string LAUNCH_SCENE_NAME = "Scenes/logo";
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnLoad() {
        Director direcor = Director.GetInstance();
        direcor.Translate(Runtime.LAUNCH_SCENE_NAME);
        return;
    }
}
}
