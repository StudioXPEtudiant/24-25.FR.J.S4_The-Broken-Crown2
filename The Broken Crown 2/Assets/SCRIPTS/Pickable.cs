using UnityEngine;

namespace StudioXP.Scripts.Objects
{
    public class Pickable : MonoBehaviour
    {
        public void PickUp()
        {
            Debug.Log($"{gameObject.name} a été ramassé !");
        }
    }
}