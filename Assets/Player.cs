using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Node currentPlayerNode;
    public Node[,] gridxy;
    public int gridX, gridY;

    public float moveSpeed = 5f;  // �̵� �ӵ� (���� ����)
    private bool isMoving = false;
    private bool isJumping = false;

    private Vector3 targetPosition;

    public void Move(Vector2Int dir)
    {
        int nextX = currentPlayerNode.GridPosition.x + dir.x;
        int nextY = currentPlayerNode.GridPosition.y + dir.y;

        // �׸��� ������ ������� üũ
        if (nextX < 0 || nextX >= gridX || nextY < 0 || nextY >= gridY)
        {
            Debug.Log("�ش� ��ġ�� �̵��� �� �����ϴ�.");
            return;
        }

        // �׸��� �迭�� ����Ͽ� �̵��� Node�� �ٷ� ã�� �� ����
        currentPlayerNode = gridxy[nextX, nextY]; // ���⼭ ������ (x, y)�� ������� ��
        targetPosition = currentPlayerNode.WorldPosition;

        // �̵��� �����ϰԲ�
        if (!isMoving) // �̵� ���� �ƴ϶��
        {
            isMoving = true;
            StartCoroutine(MoveCoroutine()); // �ڷ�ƾ�� ����
        }
    }
    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpRoutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        float journeyLength = Vector3.Distance(transform.position, targetPosition);
        float startTime = Time.time;

        // �̵��� ���� ������ �ڷ�ƾ�� ��� ����
        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(transform.position, targetPosition, fractionOfJourney);

            // �̵��� �������� ����
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }

            yield return null;
        }
    }

    private IEnumerator JumpRoutine()
    {
        float jumpHeight = 2f;
        float jumpTime = 0.5f;
        Vector3 startPos = transform.position;
        Vector3 peakPos = startPos + Vector3.up * jumpHeight;

        float elapsedTime = 0f;

        // ���
        while (elapsedTime < jumpTime)
        {
            transform.position = Vector3.Lerp(startPos, peakPos, elapsedTime / jumpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����
        elapsedTime = 0f;
        while (elapsedTime < jumpTime)
        {
            transform.position = Vector3.Lerp(peakPos, startPos, elapsedTime / jumpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        isJumping = false;
    }


    private void Update()
    {
      
    }
}
