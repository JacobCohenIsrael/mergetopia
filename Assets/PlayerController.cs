using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform pointer;
    [SerializeField] private float screenHeightPercent = 0.8f;
    [SerializeField] private float xMinMax = 3f;
    [SerializeField] private MergeableConfig mergeableConfig;
    [SerializeField] private Camera mainCamera;

    private Mergeable mergeable;
    private bool isTouching;
    private Vector2 targetPosition;

    private void Start()
    {
        CreateRandomMergeable().Forget();
        targetPosition.y = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, screenHeightPercent, 0)).y;
    }

    private async UniTask CreateRandomMergeable()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        var randomInt = Random.Range(0, 4);
        var config = mergeableConfig.mergeableBallConfigList[randomInt];
        mergeable = Instantiate(config.MergeablePrefab, pointer);
        mergeable.TurnGravityOff();
        targetPosition.x = 0f;
        pointer.position = targetPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
        }

        if (isTouching)
        {
            targetPosition.x = Mathf.Clamp(GetPointerPosition().x, -xMinMax, xMinMax);
            pointer.position = Vector2.MoveTowards(pointer.position, targetPosition, speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0) && isTouching)
        {
            isTouching = false;
            if (mergeable != null)
            {
                mergeable.transform.parent = null;
                mergeable.TurnGravityOn();
                mergeable = null;
                CreateRandomMergeable().Forget();
            }
        }
    }

    private Vector2 GetPointerPosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}