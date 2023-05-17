using Game.Interfaces;

namespace Game.CombatSystem
{
    public class BaseHitResponder : IHitResponder
    {
        private CharacterBase.Character _character;
        private HitResponse _response;

        public void Init(CharacterBase.Character character)
        {
            _character = character;

            _response = new HitResponse(HitResponseType.Success);
        }
        
        public HitResponse GetHit(HitData hitData)
        {
            return _response;
        }
    }
}
