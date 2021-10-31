public class Patient : GAgent
{
    private void Start()
    {
        base.Start();

        SubGoal subGoal01 = new SubGoal("IsWaiting" , 1 , true);
        SubGoalsList.Add(subGoal01 , 3);
    }
}
