﻿using UnityEngine;
using System.Collections;

namespace CallOfValhalla.Enemy
{
    public class Surt_Search : MonoBehaviour
    {

        [SerializeField]
        private Transform _lineCastStart;
        [SerializeField]
        private float _followTime;

        private Surt_Movement _movement;

        private Transform _transform;
        private GameObject _lineCastObject;
        private Transform _lineCastEnd;
        private int allButIgnoreLinecast = ~(1 << 8);
        private float _followTimer;


        // Use this for initialization
        void Start()
        {

            _transform = GetComponent<Transform>();
            _lineCastObject = GameObject.Find("LineCastEndingPoint");
            _lineCastEnd = _lineCastObject.GetComponent<Transform>();
            _movement = GetComponent<Surt_Movement>();
            _followTimer = _followTime;

        }

        // Update is called once per frame
        void Update()
        {
            SearchSurroundings();
        }

        private void SearchSurroundings()
        {
            var colliders = Physics2D.OverlapCircleAll(_transform.position, 20);

            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.tag == "Player")
                {
                    if (!Physics2D.Linecast(new Vector2(_lineCastStart.position.x, _lineCastStart.position.y), new Vector2(_lineCastEnd.position.x, _lineCastEnd.position.y), allButIgnoreLinecast))
                    {
                        Debug.DrawLine(new Vector2(_lineCastStart.position.x, _lineCastStart.position.y), new Vector2(_lineCastEnd.position.x, _lineCastEnd.position.y));

                        _movement.PlayerOnSight = true;
                        _followTimer = _followTime;

                    }
                    else
                    {
                        _movement.PlayerOnSight = false;
                    }
                }


            }
        }


    }
}