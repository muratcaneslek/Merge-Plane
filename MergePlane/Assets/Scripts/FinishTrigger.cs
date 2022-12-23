using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip triggerClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Plane>(out var plane))
        {
            PlayerData.Instance.AddGold(plane.goldPerTour);
            
            if(triggerClip)
            {
                AudioSource.PlayClipAtPoint(triggerClip, transform.position);
            }
        }
    }
}
