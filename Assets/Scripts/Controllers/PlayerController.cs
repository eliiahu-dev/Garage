using UnityEngine;

public class PlayerController : IController
{
    public PlayerView View { get; private set; }

    private bool activeMove;
    private float xRotation;

    public void AddView(BaseView view)
    {
        View = view as PlayerView;
    }

    public void OnStartGame()
    {
        activeMove = true;
        View.transform.position = View.GamePoint.position;
        View.NavMeshAgent.enabled = true;
    }

    private void CameraMove()
    {
        var delta = Time.deltaTime;
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        mouseX *= View.SensitivityMouse * delta;
        mouseY *= View.SensitivityMouse * delta;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        View.Player.Rotate(mouseX * Vector3.up);
        View.PlayerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    private void PlayerMove()
    {
        if (activeMove == false)
        {
            return;
        }
        
        var playerTransform = View.Player.transform;
        var movement = playerTransform.position;
        
        if (Input.GetKey(KeyCode.W))
            movement += playerTransform.forward;
        if (Input.GetKey(KeyCode.S))
            movement -= playerTransform.forward;
        if (Input.GetKey(KeyCode.A))
            movement -= playerTransform.right;
        if (Input.GetKey(KeyCode.D))
            movement += playerTransform.right;
        
        View.NavMeshAgent.destination = movement;
    }

    
    public void OnUpdate()
    {
        CameraMove();
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.E))
        {
            var itemController = MonoEntryPoint.Instance.Get<ItemController>();
            itemController.GetOrDropItem();
        }
    }
}
