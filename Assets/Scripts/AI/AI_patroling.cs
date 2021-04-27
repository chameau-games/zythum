using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_patroling : MonoBehaviour
{
    public float speed;
    public Transform[] points;
    public int _position ;

    public float startwaittime;

    private AI_FOVDetection fovd;
    private Transform player;
    private int nextPosition;
    private float waittime;
    private Animator animator;

    public bool gameover;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        fovd = GameObject.FindObjectOfType<AI_FOVDetection>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        setnextposition();
        waittime = startwaittime;
    }

    // Update is called once per frame
    void Update()
    {
        if (fovd.isPatroling)
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
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.position);
        if (Vector3.Distance(transform.position, player.position) < 1f)
        {
            animator.SetBool("iswalking",false);
            speed = 0;
            gameover = true;
            // CustomNetworkManager.singleton.ServerChangeScene("Gameover");
        }

    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[nextPosition].position, speed * Time.deltaTime);
        transform.LookAt(points[nextPosition].position);


        if (Vector3.Distance(transform.position, points[nextPosition].position) < 0.2f)
        {
            _position = nextPosition;
            if (waittime <= 0)
            {
                setnextposition();
                if (_position != nextPosition)
                {
                    animator.SetBool("iswalking",true);
                }
                waittime = startwaittime;
            }
            else
            {
                animator.SetBool("iswalking",false);
                waittime -= Time.deltaTime;
            }
        }
    }
    void setnextposition()
    {
        if (_position==0)
        {
            nextPosition = Random.Range(0, 2);
        }
        if (_position == 2)
        {
            nextPosition = Random.Range(1, 5);
            if (nextPosition == 4)
            {
                nextPosition = 6;
            }
        }
        if (_position == 4)
        {
            nextPosition = Random.Range(3,7);
            if (nextPosition == 6)
            {
                nextPosition = 7;
            }
        }
        if (_position == 7)
        {
            nextPosition = Random.Range(6,10);
            if (nextPosition == 9)
            {
                nextPosition = 4;
            }
        }
        if (_position == 5|| _position == 8)
        {
            nextPosition = Random.Range(_position-1,_position+1);
        }
        if (_position == 1 || _position == 3)
        {
            nextPosition = Random.Range(_position - 1, _position + 2);
        }
        if (_position == 6)
        {
            nextPosition = Random.Range(5,8);
            if (nextPosition == 5)
            {
                nextPosition = 2;
            }
        }
    }
}
