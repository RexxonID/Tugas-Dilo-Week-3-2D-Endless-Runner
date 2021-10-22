using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip scoreHighliht;
    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    public void PlayJump()
    {
        audioPlayer.PlayOneShot(jump);
    }
    public void playScoreHighlight()
    {
        audioPlayer.PlayOneShot(scoreHighliht);
    }
}
