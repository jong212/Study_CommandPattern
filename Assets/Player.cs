using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Node currentPlayerNode;
    public Node[,] gridxy;
    public int gridX, gridY;

    public float moveSpeed = 5f;  // 이동 속도 (조정 가능)
    private bool isMoving = false;
    private bool isJumping = false;

    private Vector3 targetPosition;

    public void Move(Vector2Int dir)
    {
        int nextX = currentPlayerNode.GridPosition.x + dir.x;
        int nextY = currentPlayerNode.GridPosition.y + dir.y;

        // 그리드 범위를 벗어나는지 체크
        if (nextX < 0 || nextX >= gridX || nextY < 0 || nextY >= gridY)
        {
            Debug.Log("해당 위치로 이동할 수 없습니다.");
            return;
        }

        // 그리드 배열을 사용하여 이동할 Node를 바로 찾을 수 있음
        currentPlayerNode = gridxy[nextX, nextY]; // 여기서 순서가 (x, y)로 맞춰줘야 함
        targetPosition = currentPlayerNode.WorldPosition;

        // 이동을 시작하게끔
        if (!isMoving) // 이동 중이 아니라면
        {
            isMoving = true;
            StartCoroutine(MoveCoroutine()); // 코루틴을 시작
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

        // 이동이 끝날 때까지 코루틴을 계속 실행
        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(transform.position, targetPosition, fractionOfJourney);

            // 이동이 끝났으면 종료
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

        // 상승
        while (elapsedTime < jumpTime)
        {
            transform.position = Vector3.Lerp(startPos, peakPos, elapsedTime / jumpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 착지
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
