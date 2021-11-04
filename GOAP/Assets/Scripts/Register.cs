using GOAP;

//TODO Done this challenge myself but just noticed that the agent works only with keys as HasArrived (Case sensitive), etc but not Arrived, etc.
//I lost the code where I have implemented above so need to find out when time permits.
public class Register : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
