using UnityEngine;

public class ItemView : BaseView
{
    [field: SerializeField] public Transform Item { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public Transform HoldPlace { get; private set; }
    [field: SerializeField] public Vector3 TakedRot { get; private set; }
    [SerializeField] private MeshRenderer placeMaterial;
    
    public bool inPlace { get; private set; }
    
    private void Start()
    {
        MonoEntryPoint.Instance.Get<ItemController>().AddView(this);
    }

    public void SetPlace()
    {
        inPlace = true;
    }
    
    public void ChangeColorPlace(bool active)
    {
        var color = active ? Color.yellow : Color.gray;
        placeMaterial.material.color = new Color(color.r, color.g, color.b, 0.5f);
    }
}