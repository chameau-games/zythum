using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = System.Random;

namespace AI
{
    public class Patrolling : MonoBehaviour
    {
        public Transform[] points;

        private FOVDetection _fovd;
        public NavMeshAgent guard;
        private Transform _nextPosition;
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
            setnextposition();
        }

        // Update is called once per frame
        void Update()
        {
            if(_fovd.GetTarget() == null)
            {
                _animator.SetBool("iswalking",true);
                Patrol();
            }
            else
            {
                ChasePlayer(_fovd.GetTarget());
            }
            
        }

        private void ChasePlayer(Transform target)
            {
                _animator.SetBool("iswalking",true);
                guard.SetDestination(target.position);
                transform.LookAt(target.position);
                if (Vector3.Distance(transform.position, target.position) < 1f)
                {
                    _animator.SetBool("iswalking",false);
                    Hashtable prop = PhotonNetwork.CurrentRoom.CustomProperties;
                    prop.Add("hasWin", false);
                    PhotonNetwork.CurrentRoom.SetCustomProperties(prop);
                    PhotonNetwork.LoadLevel("Gameover");
                    }
            }

        void Patrol()
        {
            guard.SetDestination(_nextPosition.position);
            transform.LookAt(transform.forward);
            
            if (Vector3.Distance(transform.position, _nextPosition.position) <= 1f)
            {
                setnextposition();
            }
        }

        void setnextposition()
        {
            Random r = new Random();
            int i = r.Next(0,points.Length);
            _nextPosition = points[i];
        }
    }
}
