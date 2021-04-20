namespace Tasks
{
    public abstract class Task
    {
        private string _description;

        public abstract void OnStart();
        public abstract void OnFinished();
        
    }
}