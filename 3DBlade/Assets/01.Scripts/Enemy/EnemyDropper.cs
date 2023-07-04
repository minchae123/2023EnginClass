using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDropper : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float maxDistance = 20f;

    public UnityEvent OnDropComplete = null;

    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void ReadyToDrop(Vector3 pos)
    {
        SetNavInfo(false);
        transform.position = pos;
    }

    private void SetNavInfo(bool value)
    {
        enemyController.NavMovement.enabled = value;
        enemyController.NavMovement.NavAgent.updatePosition = value;
        enemyController.NavMovement.NavAgent.updateRotation= value;
        enemyController.NavMovement.NavAgent.enabled = value;
    }

    public bool Drop()
    {
        Vector3 curPos = transform.position;
        RaycastHit hit;
        bool isHit = Physics.Raycast(curPos, Vector3.down, out hit, maxDistance, whatIsGround);

        if(isHit)
        {
            DropDecal decal = PoolManager.Instance.Pop("GroundDecal") as DropDecal;
            decal.transform.position = hit.point + new Vector3(0, 2f, 0);
            decal.SetUpSize(new Vector3(3, 3, 4));

            decal.StartSeq(() =>
            {
                decal.FadeOut(0.3f);
                transform.DOMove(hit.point, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    SetNavInfo(true);
                    OnDropComplete?.Invoke();
                });
            });
            return true;
        }
        else
            return false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            ReadyToDrop(new Vector3(6, 11, 11));
            Drop();
        }
    }
}