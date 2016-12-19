using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Dialogue
{
    public class NPC : MonoBehaviour
    {
        private Sprite _odinSprite;
        private Sprite _thorSprite;
        private Sprite _thorHammerSprite;
        

        private SpriteRenderer _renderer;
        // Use this for initialization
        void Awake()
        {
            _thorSprite = Resources.Load("NPC/ThorNoMjölnir", typeof(Sprite)) as Sprite;
            _odinSprite = Resources.Load("NPC/Odin", typeof(Sprite)) as Sprite;
            _thorHammerSprite = Resources.Load("NPC/ThorMjölnir", typeof(Sprite)) as Sprite;
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetOdin()
        {
            _renderer.sprite = _odinSprite;
        }

        public void SetThor()
        {
            _renderer.sprite = _thorSprite;
        }

        public void SetThorHammer()
        {
            _renderer.sprite = _thorHammerSprite;
        }
    }
}