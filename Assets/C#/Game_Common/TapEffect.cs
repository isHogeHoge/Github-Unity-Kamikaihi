using UnityEngine;
using UnityEngine.UIElements;

public class TapEffect : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject stageManager;
    private CancelClickOutOfScreen cancelClickCnt;
    private ParticleSystem[] array;

    private void Start()
    {
        cancelClickCnt = stageManager.GetComponent<CancelClickOutOfScreen>();
        array = GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        // タップした場所(ワールド座標)にタップエフェクトを再生
        if (Input.GetMouseButtonDown(0) && cancelClickCnt.isWithinTheGameScreen())
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;

            transform.position = _camera.ScreenToWorldPoint(pos);
            for(var i = 0; i < array.Length; i++)
            {
                array[i].Play();

            }
        }
    }
}
