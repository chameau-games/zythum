﻿using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace AI
{
    public class Patrolling : MonoBehaviour
    {
        public float speed;
        public Transform[] points;
        public int position ;

        public float startwaittime;

        private FOVDetection _fovd;
        private Transform _player;
        private int _nextPosition;
        private float _waittime;
        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                enabled = false;
                return;
            }
            _animator=GetComponent<Animator>();
            _fovd = GameObject.FindObjectOfType<FOVDetection>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            setnextposition();
            _waittime = startwaittime;
        }

        // Update is called once per frame
        void Update()
        {
            if (_fovd.isPatroling)
            {
                Patrol();
            }
            else
            {
                ChasePlayer();
            }
        }

        private void ChasePlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
            transform.LookAt(_player.position);
            if (Vector3.Distance(transform.position, _player.position) < 1f)
            {
                _animator.SetBool("iswalking",false);
                speed = 0;
                Hashtable prop = PhotonNetwork.CurrentRoom.CustomProperties;
                prop.Add("hasWin", false);
                PhotonNetwork.CurrentRoom.SetCustomProperties(prop);
                PhotonNetwork.LoadLevel("Gameover");
            }

        }

        void Patrol()
        {
            transform.position = Vector3.MoveTowards(transform.position, points[_nextPosition].position, speed * Time.deltaTime);
            transform.LookAt(points[_nextPosition].position);


            if (Vector3.Distance(transform.position, points[_nextPosition].position) < 0.2f)
            {
                position = _nextPosition;
                if (_waittime <= 0)
                {
                    setnextposition();
                    if (position != _nextPosition)
                    {
                        _animator.SetBool("iswalking",true);
                    }
                    _waittime = startwaittime;
                }
                else
                {
                    _animator.SetBool("iswalking",false);
                    _waittime -= Time.deltaTime;
                }
            }
        }
        void setnextposition()
        {
            if (position==0)
            {
                _nextPosition = Random.Range(0, 2);
            }
            if (position == 2)
            {
                _nextPosition = Random.Range(1, 5);
                if (_nextPosition == 4)
                {
                    _nextPosition = 6;
                }
            }
            if (position == 4)
            {
                _nextPosition = Random.Range(3,7);
                if (_nextPosition == 6)
                {
                    _nextPosition = 7;
                }
            }
            if (position == 7)
            {
                _nextPosition = Random.Range(6,10);
                if (_nextPosition == 9)
                {
                    _nextPosition = 4;
                }
            }
            if (position == 5|| position == 8)
            {
                _nextPosition = Random.Range(position-1,position+1);
            }
            if (position == 1 || position == 3)
            {
                _nextPosition = Random.Range(position - 1, position + 2);
            }
            if (position == 6)
            {
                _nextPosition = Random.Range(5,8);
                if (_nextPosition == 5)
                {
                    _nextPosition = 2;
                }
            }
        }
    }
}
