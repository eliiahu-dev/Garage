using UnityEngine;

public class StoryView : MonoBehaviour
{
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip StepSound;
    [SerializeField] private AudioClip DoorSound;
    [SerializeField] private AudioClip CarSound;
    [SerializeField] private Animator ActionAnim;

    public void SetEndAnim()
    {
        ActionAnim.Play("End");
    }
    
    public void PlayStep()
    {
        AudioSource.clip = StepSound;
        AudioSource.Play();
    }
    
    public void PlayOpen()
    {
        AudioSource.volume = 0.2f;
        AudioSource.clip = DoorSound;
        AudioSource.Play();
    }
    
    public void PlayCar()
    {
        AudioSource.volume = 0.3f;
        AudioSource.clip = CarSound;
        AudioSource.Play();
    }

    public void StartGame()
    {
        MusicSource.volume = 0.1f;
        var playerController = MonoEntryPoint.Instance.Get<PlayerController>();
        playerController.OnStartGame();
    }
}