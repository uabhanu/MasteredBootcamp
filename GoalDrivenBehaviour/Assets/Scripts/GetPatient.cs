using GOAP;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject _resourceObj;
    public override bool PrePerform()
    {
        _resourceObj = GWorld.Instance.RemoveCubicle();

        if(_resourceObj != null)
        {
            GInventory.AddGameObject(_resourceObj);
        }
        else
        {
            GWorld.Instance.AddPatient(target);
            target = null;
            return false;
        }

        target = GWorld.Instance.RemovePatient();

        if(target == null)
        {
            return false;
        }
        
        GWorld.Instance.GetWorld().ModifyState("FreeCubicle" , -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("Waiting" , -1);

        if(target)
        {
            target.GetComponent<GAgent>().inventory.AddGameObject(_resourceObj);
        }
        
        return true;
    }
}
