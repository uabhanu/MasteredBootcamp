using GOAP;

public class Nurse : GAgent
{
    private void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("TreatPatient" , 1 , true);
        goals.Add(s1 , 3); 
    }
}
