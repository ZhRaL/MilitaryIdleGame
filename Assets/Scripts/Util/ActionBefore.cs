using System;

namespace Util
{
    public class ActionBefore
    {
        public float time;
        public Action Action;

        public ActionBefore(float time, Action action)
        {
            this.time = time;
            Action = action;
        }
    }
}