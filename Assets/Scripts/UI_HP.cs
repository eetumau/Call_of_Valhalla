using UnityEngine;
using System.Collections;
using CallOfValhalla.Player;
using UnityEngine.UI;

namespace CallOfValhalla.UI {
    public class UI_HP : MonoBehaviour {

        private Transform _transform;
        private Player_HP _hp;
        private Image _image;
        private float _sizeBefore;

        // Use this for initialization
        void Start() {
            _hp = FindObjectOfType<Player_HP>();
            _image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update() {
            _image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _hp.Instance.HP * 40);
        }
    }
}