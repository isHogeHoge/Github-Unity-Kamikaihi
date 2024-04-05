using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class TeacherController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerAndTeacher;
    private const float moveSpeed = -2.0f;
    private bool isMoving = true;


    private void Update()
    {
        if (isMoving)
        {
            this.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 停止位置まで移動したら
        if (col.tag == "Stop")
        {
            // 移動ストップ & アニメーションの切り替え
            isMoving = false;   // 移動ストップ
            Animator animator = this.GetComponent<Animator>();
            animator.Play("TeacherStop");
        }

    }

    // 停止アニメーション後、Player&Teacherの切り替え
    private void PlayPlayerAndTeacherAnima()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        playerAndTeacher.SetActive(true);
    }
}
