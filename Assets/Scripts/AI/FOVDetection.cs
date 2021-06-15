using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class FOVDetection : MonoBehaviour
    {
        private GameObject[] _players;
        public float maxAngle;
        public float maxRadius;
        public LayerMask map;
        private Transform target;
        private LayerMask layermask;
        
        // Start is called before the first frame update
        void Start()
        {
            layermask = 1 << 11;
            layermask = ~layermask;
            if (!PhotonNetwork.IsMasterClient)
            {
                enabled = false;
                return;
            }

            target = null;
        }

        // Update is called once per frame
        void Update()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in _players)
            {
                if (checkingFOV(player.transform, maxAngle, maxRadius))
                {
                    target = player.transform;
                }
            }
        }

        public Transform GetTarget()
        {
            return target;
        }
    
        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxRadius);

            Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);

            if (!_isInFov)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
        }*/
        private bool checkingFOV(Transform target, float maxAngle, float maxRadius)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, maxRadius, layermask);
            foreach (Collider col in cols)
            {
                if (col.transform == target)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, target.position, out hit,
                        Vector3.Distance(transform.position, target.position), layermask))
                    {
                        if (hit.collider.transform == target)
                        {
                            Debug.Log("ici");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
    
}
