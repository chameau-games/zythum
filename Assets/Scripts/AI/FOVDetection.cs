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
        public Transform target;

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
                _isInFov = inFOV(transform,p.transform , maxAngle, maxRadius);
                if (_isInFov)
                {
                    Debug.Log("a trouvé un joueur");
                    isPatroling = false;
                    target = p.transform;
                }
                else
                {
                    Debug.Log("n'as pas trouvé de joueur");
                    isPatroling = true;
                }
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
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
        }*/
        public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
        {
            Collider[] overlaps = new Collider[100];
            int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);
            Debug.Log("test1");

            for (int i = 0; i < count + 1; i++)
            {

                if (i < overlaps.Length && overlaps[i] != null)
                {

                    if (overlaps[i].transform == target)
                    {

                        Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                        directionBetween.y *= 0;
                        Debug.Log("test2");

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
