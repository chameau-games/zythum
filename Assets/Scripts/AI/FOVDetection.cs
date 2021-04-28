using Photon.Pun;
using UnityEngine;

namespace AI
{
    public class FOVDetection : MonoBehaviour
    {
        public bool isPatroling;
        private GameObject[] _players;
        public float maxAngle;
        public float maxRadius;

        private bool _isInFov = false;
        // Start is called before the first frame update
        void Start()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                enabled = false;
                return;
            }
            isPatroling = true;
            _players = GameObject.FindGameObjectsWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            _isInFov = false;
            foreach (GameObject player in _players)
            {
                if (inFOV(transform, player.transform, maxAngle, maxRadius))
                    _isInFov = true;
            }
            
            if (_isInFov)
            {
                isPatroling = false;
            }
            else
            {
                isPatroling = true;
            }
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
            Gizmos.DrawRay(transform.position, (_player.position - transform.position).normalized * maxRadius);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
        }*/
        public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
        {

            Collider[] overlaps = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

            for (int i = 0; i < count + 1; i++)
            {

                if (i < overlaps.Length && overlaps[i] != null)
                {

                    if (overlaps[i].transform == target)
                    {

                        Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                        directionBetween.y *= 0;

                        float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                        if (angle <= maxAngle)
                        {

                            Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, maxRadius))
                            {

                                if (hit.transform == target)
                                    return true;

                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
