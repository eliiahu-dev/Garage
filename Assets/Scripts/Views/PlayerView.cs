using UnityEngine;
using UnityEngine.AI;

public class PlayerView : BaseView
{
    [field: SerializeField] public StoryView Story { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public Transform PlayerCamera { get; private set; }
    [field: SerializeField] public Transform GamePoint { get; private set; }
    [field: SerializeField] public Transform ItemPoint { get; private set; }
    [field: SerializeField] public float SensitivityMouse  { get; private set; }
    [field: SerializeField] public float StartGameDelay  { get; private set; }

    private void Start()
    {
        MonoEntryPoint.Instance.Get<PlayerController>().AddView(this);
    }
}
