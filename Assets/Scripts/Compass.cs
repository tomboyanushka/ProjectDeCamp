using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Transform Player;
    public Vector3 NorthDirection;

    public RectTransform NorthLayer;
	
	// Update is called once per frame
	void Update ()
    {
        ChangeNorth();
	}

    public void ChangeNorth()
    {
        //Vector3 dir = NorthDirection;
        NorthDirection.z = Player.eulerAngles.y;
        //NorthLayer.localEulerAngles = Vector3.Slerp(dir, NorthDirection, Time.deltaTime); //NorthDirection;
        NorthLayer.localEulerAngles = NorthDirection;
    }
}
