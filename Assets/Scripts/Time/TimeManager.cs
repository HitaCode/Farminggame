using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [SerializeField]
    GameTimeStamp timestamp;

    public float timeScale = 1.0f;

    [Header ("Day and Night cycle")]

    public Transform sunTransform;

    List<ITimeTracker> listeners = new List<ITimeTracker>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timestamp = new GameTimeStamp(0, GameTimeStamp.Season.Spring, 1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1 / timeScale);
           
        }
        
    }

    public void Tick()
    {
        timestamp.UpdateClock();

        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }
        UpdateSunMovement();
    }
    public void SkipTime(GameTimeStamp timeToSkipTo)
    {
        int timeToSkipInMinutes = GameTimeStamp.TimestampInMinutes(timeToSkipTo);
        Debug.Log("Time to skip to:" + timeToSkipInMinutes);
        int timeNowInMinutes = GameTimeStamp.TimestampInMinutes(timestamp);
        Debug.Log("Time now:" + timeNowInMinutes);

        int differenceInMinutes = timeToSkipInMinutes - timeNowInMinutes;
        Debug.Log(differenceInMinutes + " minutes will be advanced");

        if (differenceInMinutes <= 0) return;
        for (int i = 0; i<differenceInMinutes; i++)
        {
            Tick();
        }
    }

    void UpdateSunMovement()
    {

        int timeInMinutes = GameTimeStamp.HoursToMinutes(timestamp.hour) + timestamp.minute;

        float sunAngle = .25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    public GameTimeStamp GetGameTimeStamp()
    {
        return new GameTimeStamp(timestamp);
    }

    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
