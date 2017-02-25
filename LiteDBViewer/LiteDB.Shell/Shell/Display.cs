namespace LiteDB.Shell
{
    public class Display
    {
        public BsonValue LastResult { get; private set; }

        public void WriteResult(BsonValue result)
        {
            LastResult = result;
        }
    }
}