﻿using GOAP;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting" , 1);
        GWorld.Instance.AddPatient(gameObject);
        return true;
    }
}
