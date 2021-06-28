using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource MainTheme;
    public AudioSource larry_Theme;
    public AudioSource sophia_Theme;
    public AudioSource steve_Theme;
    public AudioSource dante_Theme;



    public AudioSource policeKnock;
    public AudioSource policeCheck;
    public AudioSource policeAlerted;

    public List<AudioSource> diceImpact;
    public List<AudioSource> handSwipe;

    private AudioSource currentTrack;
    // Start is called before the first frame update
    void Start()
    {

        //DontDestroyOnLoad(currentTrack);
    }


    public void switchTrack(AudioSource nextTrack)
    {
        if (currentTrack != null)
            currentTrack.Stop();

        currentTrack = Instantiate(nextTrack);
        currentTrack.Play();
        currentTrack.loop = true;
    }

    public void playMainTheme()
    {
        switchTrack(MainTheme);
    }
    public void playResultsTheme()
    {
        
    }
    public void playPrototypeTheme()
    {
        
    }

    private GameObject soundObject = null;
    private AudioSource audioSource = null;

    void playSound(AudioSource source, float volume)
    {
        if (soundObject == null)
        {
            soundObject = new GameObject("sound");
            audioSource = soundObject.AddComponent<AudioSource>();
            Debug.Log("sound object instance created");
        }

        audioSource.PlayOneShot(source.clip, volume);

        //source.gameObject.SetActive(true);// = true;
        //source.PlayOneShot(source.clip);
        // Destroy(sound.gameObject, 1.0f);
    }
    public AudioSource pickAsound(List<AudioSource> other)
    {
        AudioSource tmp;
        int i = Random.Range(0, other.Count);
        tmp = other[i];

        return tmp;
    }

    public void playHandSwipe()
    {
        //pick sond from list

        playSound(pickAsound(handSwipe), 0.5f);
    }
    
    public void playHackFullCharge()
    {
        
    }

}