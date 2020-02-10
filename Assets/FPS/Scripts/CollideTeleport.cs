using UnityEngine;

// Debug script, teleports the player across the map for faster testing
public class CollideTeleport : MonoBehaviour
{
    /*public KeyCode activateKey1 = KeyCode.F12;
    public KeyCode activateKey2 = KeyCode.F12;*/
    public Transform teleportTarget;
    public GameObject player;

    //PlayerCharacterController m_PlayerCharacterController;

    /*void Awake()
    {
        m_PlayerCharacterController = FindObjectOfType<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, Teleport>(m_PlayerCharacterController, this);
        
    }*/

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = teleportTarget.transform.position;
    }


    /*void Update()
    {
        if (Input.GetKeyDown(activateKey1))
        {
            Debug.Log(transform.position, teleportTarget);
            m_PlayerCharacterController.transform.SetPositionAndRotation(teleportTarget.transform.position, transform.rotation);
            //m_PlayerCharacterController.transform.SetPositionAndRotation(transform.position, transform.rotation);
            Health playerHealth = m_PlayerCharacterController.GetComponent<Health>();
            if(playerHealth)
            {
                playerHealth.Heal(999);
            }
            Debug.Log(transform.position);
        }
        if (Input.GetKeyDown(activateKey2))
        {
            player.transform.position = teleportTarget.transform.position;
            //player.transform.position = new Vector3(122.1098, 190.001, 9.063971);
        }
    }*/

}
