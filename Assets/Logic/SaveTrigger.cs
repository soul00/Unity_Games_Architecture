using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Logic
{
  public class SaveTrigger : MonoBehaviour
  {
    private ISaveLoadService _saveLoadService;

    public BoxCollider Collider;

    private void Awake()
    {
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    private void OnTriggerEnter(Collider other)
    {
      _saveLoadService.SaveProgress();
      Debug.Log("Save progress");
      gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
      if (Collider == null)
        return;

      Gizmos.color = Color.red;
      Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
    }
  }
}
