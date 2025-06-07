using UnityEngine;

//This is something Architects liblary uses for reasons, i don't make the name i just follow it. below note from it:


/*
* This stuff is normally isn't necessary, but Mono's delegates are bugged and cant be persistant outside of the Unity Editor.
* For a workaround, we need to add the listeners in a Start() method so they get called late enough for Unity to not bother
* itself with it being persistant or not.
*/

namespace RoyalCommonalities.MonoBehaviours;

internal class
    DoRandomShitCuzNoUnityEditorAndMonoSucksAndDoesntHavePersistantDelegatesOnRuntime : MonoBehaviour
{
    private Drillable _drillable;

    private void Start()
    {
        _drillable = GetComponent<Drillable>();
        GetComponent<GenericHandTarget>().onHandHover.AddListener(_ => _drillable.HoverDrillable());
    }
}