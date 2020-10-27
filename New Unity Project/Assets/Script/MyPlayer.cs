using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MyPlayer : MonoBehaviourPun, IPunObservable
{

    public PhotonView pv;
    public float moveSpeed = 10f;
    public float jumpforce = 600f;

    private Vector3 smoothMove;


    private GameObject sceneCamera;
    public GameObject playerCamera;

    public SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool IsGrounded;

    void Start()
    {
      //  PhotonNetwork.SendRate = 20;
      //  PhotonNetwork.SerializationRate = 15;
        if (photonView.IsMine)
        {
            rb = GetComponent<Rigidbody2D>();
            playerCamera = GameObject.Find("Main Camera");

            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
        else
        {
            smoothMovement();
        }
    }

    private void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void ProcessInputs()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.flipX = false;
            pv.RPC("OnDirectionChange_RIGHT", RpcTarget.Others);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            pv.RPC("OnDirectionChange_LEFT", RpcTarget.Others);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    [PunRPC]
    void OnDirectionChange_LEFT()
    {
        sr.flipX = true;

    }

    [PunRPC]
    void OnDirectionChange_RIGHT()
    {
        sr.flipX = false;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (photonView.IsMine)
        {
            if (col.gameObject.tag == "Ground")
            {
                IsGrounded = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (photonView.IsMine)
        {
            if (col.gameObject.tag == "Ground")
            {
                IsGrounded = false;
            }
        }
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpforce);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
        }
    }
}
