using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = System.Random;

namespace AI
{
    public class Patrolling : MonoBehaviour
    {
        public float speed;
        public Transform[] points;

        public float startwaittime;

        private FOVDetection _fovd;
        private Transform _nextPosition;
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
            setnextposition();
            _waittime = startwaittime;
        }

        // Update is called once per frame
        void Update()
        {
            if (_fovd.target==null)
            {
                Patrol();
            }
            else
            {
                ChasePlayer(_fovd.target);
            }
        }

        private void ChasePlayer(Transform target)
        {
            _animator.SetBool("iswalking",true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                transform.LookAt(target.position);
                if (Vector3.Distance(transform.position, target.position) < 1f)
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
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition.position, speed * Time.deltaTime);
            transform.LookAt(_nextPosition.position);


            if (Vector3.Distance(transform.position, _nextPosition.position) < 0.2f)
            {
                if (_waittime <= 0)
                {
                    setnextposition();
                    _animator.SetBool("iswalking", true);
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
            List<Transform> nextpoints =new List<Transform>();

            foreach (var p in points)
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position, p.position,Color.red);
                if (!(Physics.Raycast(transform.position, p.position, out hit)))
                {
                    nextpoints.Add(p);
                }
            }
            Random r = new Random();
            int i = r.Next(nextpoints.Count);
            _nextPosition = nextpoints[i];
        }
    }
}
