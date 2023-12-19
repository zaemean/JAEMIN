using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Music : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;
    //The number of seconds for each song beat
    public float secPerBeat;
    //Current song position, in seconds
    public float songPosition;
    //Current song position, in beats
    public float songPositionInBeats;
    //How many seconds have passed since the song started
    public float dspSongTime;
    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;
    public Transform[] NotePlace;
    public GameObject Note;
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        //Start the music
        musicSource.Play();
        InvokeRepeating("makeNote",0f, secPerBeat);
        Debug.Log(GameManager.gameManager.ReturnDifficulty());
        switch (GameManager.gameManager.ReturnDifficulty()) {
            case 0:
                InvokeRepeating("makeNote", 0f, secPerBeat * 8f);
                break;
            case 1:
                InvokeRepeating("makeNote", 0f, secPerBeat * 4f);
                break;
            case 2:
                InvokeRepeating("makeNote", 0f, secPerBeat * 2f);
                break;
            case 3:
                InvokeRepeating("makeNote", 0f, secPerBeat * 1f);
                break;
            case 4:
                InvokeRepeating("makeNote", 0f, secPerBeat * 0.5f);
                break;
        }
        
    }
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        if (songPosition > 127){
            CancelInvoke();
            if (songPosition > 132) GameManager.gameManager.showResult();
        }
    }
    void makeNote() {
        int ranIndex = Random.Range(0, 4);
        Instantiate(Note,NotePlace[ranIndex].position, Quaternion.Euler(0,0, 180 - ranIndex*90));
    }
}
