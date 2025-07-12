using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skill004 : Skill
{
    public GameObject rotatingObjectPrefab; // 회전시킬 오브젝트 프리팹

    public float radius = 6f;
    public float heightOffset = 0.6f;
    public float rotationSpeed = 360f;       // 도/초
    public float rotationDuration = 5f;

    private List<Transform> rotatingObjects = new List<Transform>();
    private Vector3 center;

    private float rotationTimer = 0f;
    private float attackCooldownTimer = 0f;
    private int currentCount = 0;

    void Start()
    {
        currentCount = (int)totalAtkCount;
        center = transform.position + new Vector3(0f, heightOffset, 0f);
        CreateRotatingObjects((int)totalAtkCount);
    }
    public override void Attack()
    {
        center = transform.position + new Vector3(0f, heightOffset, 0f);
        rotationTimer = rotationDuration;

        // 오브젝트 보이게 만들기
        foreach (Transform obj in rotatingObjects)
        {
            obj.gameObject.SetActive(true);
            obj.GetComponent<Sawtooth>().damage = totalAtk;
            obj.GetComponent<Sawtooth>().ultmit = ultmit;
            obj.GetComponent<Sawtooth>().target = FindClosestEnemy();
        }
    }

    private void CreateRotatingObjects(int count)
    {
        rotatingObjects.Clear();
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 localPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            Vector3 finalLocalPos = new Vector3(localPos.x, localPos.y + heightOffset, localPos.z);

            GameObject obj = Instantiate(rotatingObjectPrefab, this.transform);
            obj.transform.localPosition = finalLocalPos;
            obj.SetActive(false); // 처음엔 숨김 상태

            rotatingObjects.Add(obj.transform);
        }
    }
    protected override void Update()
    {
        // 10초마다 공격
        attackCooldownTimer += Time.deltaTime;
        if (attackCooldownTimer >= totalCooltime)
        {
            attackCooldownTimer = 0f;
            Attack();
        }

        // 회전 중
        if (rotationTimer > 0f)
        {
            rotationTimer -= Time.deltaTime;

            float angleStep = 360f / totalAtkCount;
            float angleOffset = Time.time * rotationSpeed;

            for (int i = 0; i < rotatingObjects.Count; i++)
            {
                float angle = (i * angleStep + angleOffset) * Mathf.Deg2Rad;

                Vector3 localPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
                rotatingObjects[i].localPosition = new Vector3(localPos.x, localPos.y + heightOffset, localPos.z);
            }

            // 회전 끝났는지 체크해서 숨김
            if (rotationTimer <= 0f)
            {
                foreach (Transform obj in rotatingObjects)
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }

        // 개수 증가 체크
        if (totalAtkCount > currentCount)
        {
            int added = (int)totalAtkCount - currentCount;
            AddNewRotatingObjects(added);
            currentCount = (int)totalAtkCount;
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (soSkill.isSkill1) { bonusCooltime = 2; }
        if (soSkill.isSkill2) { bonusAtkCount = 1; }
        if (soSkill.isSkill3) { ultmit = true; }
    }

    private void AddNewRotatingObjects(int addedCount)
    {
        float angleStep = 360f / totalAtkCount;

        // 기존 오브젝트 재배치
        for (int i = 0; i < rotatingObjects.Count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 pos = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            rotatingObjects[i].position = pos;
        }

        // 새 오브젝트 생성
        for (int i = rotatingObjects.Count; i < totalAtkCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 pos = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;

            GameObject obj = Instantiate(rotatingObjectPrefab, pos, Quaternion.identity, this.transform); // ✅ 자식으로 생성
            rotatingObjects.Add(obj.transform);
        }
    }
}
