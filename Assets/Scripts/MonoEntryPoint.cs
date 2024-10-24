using System.Collections.Generic;
using UnityEngine;

public class MonoEntryPoint : MonoBehaviour
{
    public static MonoEntryPoint Instance { get; private set; }
    private List<IController> controllers = new();

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Instance = this;
        InitControllers();
    }

    private void InitControllers()
    {
        AddController(new PlayerController());
        AddController(new ItemController());
    }

    public T Get<T>() where T : IController
    {
        foreach (var controller in controllers)
        {
            if (controller is T needController)
            {
                return needController;
            }
        }

        Debug.LogError($"Find null controller {typeof(T)}");
        return default;
    }
    
    private void AddController(IController controller)
    {
        controllers.Add(controller);
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            controller.OnUpdate();
        }
    }
}
