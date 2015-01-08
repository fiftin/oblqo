namespace Oblakoo.Tasks
{
    public class CreateFolderTask : AsyncTask
    {
        public CreateFolderTask(AsyncTaskType type, object options, int priority, AsyncTask parent) 
            : base(type, options, priority, parent)
        {
        }
    }
}
