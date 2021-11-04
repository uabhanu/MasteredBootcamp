// Reason to use sealed is to queue things up and avoid any conflicts in the process. May need to do more research on this
namespace GOAP
{
    public sealed class GWorld
    {
        private static readonly GWorld Instance = new GWorld();
        private static WorldStates _worldStates;

        static GWorld()
        {
            _worldStates = new WorldStates();
        }

        private GWorld() { }

        public static GWorld WorldInstance
        {
            get { return Instance; }
        }

        public WorldStates GetWorld()
        {
            return _worldStates;
        }
    }
}
