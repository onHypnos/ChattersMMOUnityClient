namespace Chatters.Services.EntityID
{
    public class EntityIdNumerator
    {
        private int _currentID = 10000;
        public int GetNewID() => _currentID++;
    }
}