﻿public class Patient : GAgent {

    new void Start() {
        base.Start();
        SubGoal s1 = new SubGoal("isWaiting", 1, true);
        goals.Add(s1, 3);
    }

}