using UnityEngine;

[RequireComponent(typeof(Pickup))]
public class WeaponPickup : MonoBehaviour
{
    [Tooltip("The prefab for the weapon that will be added to the player on pickup")]
    public WeaponController weaponPrefab;

    Pickup m_Pickup;

    void Start()
    {
        m_Pickup = GetComponent<Pickup>();
        DebugUtility.HandleErrorIfNullGetComponent<Pickup, WeaponPickup>(m_Pickup, this, gameObject);

        // Subscribe to pickup action
        m_Pickup.onPick += OnPickedV2;

        // Set all children layers to default (to prefent seeing weapons through meshes)
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                t.gameObject.layer = 0;
        }
    }

    //original base pickup method
    /*void OnPicked(PlayerCharacterController byPlayer)
    {
        PlayerWeaponsManager playerWeaponsManager = byPlayer.GetComponent<PlayerWeaponsManager>();
        if (playerWeaponsManager)
        {
            if (playerWeaponsManager.AddWeapon(weaponPrefab))
            {
                // Handle auto-switching to weapon if no weapons currently
                if (playerWeaponsManager.GetActiveWeapon() == null)
                {
                    playerWeaponsManager.SwitchWeapon(true);
                }

                m_Pickup.PlayPickupFeedback();

                Destroy(gameObject);
            }
        }
    }*/

    //modified original method that applies a pickup to both hands at once. 
    void OnPicked(PlayerCharacterController byPlayer)
    {
        PlayerWeaponsManager[] playerWeaponsManager = byPlayer.GetComponents<PlayerWeaponsManager>();

        for (int i = 0; i < playerWeaponsManager.Length; i++)
        {
            if (playerWeaponsManager[i])
            {
                if (playerWeaponsManager[i].AddWeapon(weaponPrefab))
                {
                    // Handle auto-switching to weapon if no weapons currently
                    if (playerWeaponsManager[i].GetActiveWeapon() == null)
                    {
                        playerWeaponsManager[i].SwitchWeapon(true);
                    }

                    m_Pickup.PlayPickupFeedback();


                }
            }
        }
        Destroy(gameObject);
    }


    //New pickup method that applies to pickup to only one hand. Checks right hand first and then left if right already has it. 
    void OnPickedV2(PlayerCharacterController byPlayer)
    {
        PlayerWeaponsManager[] playerWeaponsManager = byPlayer.GetComponents<PlayerWeaponsManager>();
        bool leftProcessed = false;
        bool rightProcessed = false;

        while (leftProcessed == false && rightProcessed == false)
        {

            if (playerWeaponsManager[0].hand == PlayerWeaponsManager.Hand.Right) //we want to process the right hand first
            {
                if (playerWeaponsManager[0].AddWeapon(weaponPrefab)) //check right hand, if right hand does not have weapon, equip it and break. If not, move on to left. 
                {
                    m_Pickup.PlayPickupFeedback();
                    break;
                }
                rightProcessed = true;
            }
            else if (playerWeaponsManager[0].hand == PlayerWeaponsManager.Hand.Left && rightProcessed == true) //we only want to process the left hand after processing the right hand and only if right hand did not get a weapon equip
            {

                if (playerWeaponsManager[0].AddWeapon(weaponPrefab)) //check Left hand, if left hand does not have weapon, equip it and break. 
                {
                    m_Pickup.PlayPickupFeedback();
                    break;
                }
                leftProcessed = true;
            }


            //Now we process the next hand. Doesn't really matter which one it is. if right was processed first, we want to do left anyway, and if we skipped the first one because it was left, we want to do right anyway
            if (playerWeaponsManager[1].AddWeapon(weaponPrefab)) //check hand, if hand does not have weapon, equip it and break. 
            {
                m_Pickup.PlayPickupFeedback();
                break;
            }
            if (playerWeaponsManager[1].hand == PlayerWeaponsManager.Hand.Left)
            {
                leftProcessed = true;
            }
            else rightProcessed = true;
        }
        Destroy(gameObject);
    }
}
