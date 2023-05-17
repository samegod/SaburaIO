namespace Game.CombatSystem
{
    public enum HitResponseType
    {
        Success,
        Block,
        Ignore,
        NoResponse,
    }
    
    public class HitResponse
    {
        public HitResponseType ResponseType;

        public HitResponse()
        {
            ResponseType = HitResponseType.Ignore;
        }

        public HitResponse(HitResponseType type)
        {
            ResponseType = type;
        }
    }
}
