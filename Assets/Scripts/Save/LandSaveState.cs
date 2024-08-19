using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LandSaveState
{
    public Land.LandStatus landStatus;
    public GameTimeStamp lastWatered;
    public Land.FarmObstacleStatus obstacleStatus;

    public LandSaveState(Land.LandStatus landStatus, GameTimeStamp lastWatered, Land.FarmObstacleStatus obstacleStatus)
    {
        this.landStatus = landStatus;
        this.lastWatered = lastWatered;
        this.obstacleStatus = obstacleStatus;
    }
    public void ClockUpdate(GameTimeStamp timestamp)
    {
        if (landStatus == Land.LandStatus.Watered)
        {
            int hoursElapsed = GameTimeStamp.CompareTimestamps(lastWatered, timestamp);
            Debug.Log(hoursElapsed + " since this was watered");
            if (hoursElapsed > 24)
            {
                landStatus = Land.LandStatus.Farmland;
            }
        }
    }
}
