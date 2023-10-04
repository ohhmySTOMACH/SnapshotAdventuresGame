using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAction : MonoBehaviour
{
    
    public float flySpeed = 5f; // Speed at which the object flies away
    public float fadeSpeed = 1f; // Speed at which the object fades out
    private const string PLAYER_CLOSE_TRIGGER = "PlayerCloseTrigger";
    private const string RUNAWAY_ACTION_TAG = "runaway";
    private Animator animator;
    private float fadeAlpha = 1f;
    
    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void RunAway() {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!currentStateInfo.IsTag(RUNAWAY_ACTION_TAG)) {
            animator.SetTrigger(PLAYER_CLOSE_TRIGGER);

            transform.Translate(new Vector3(0, flySpeed, flySpeed) * Time.deltaTime);

            fadeAlpha -= fadeSpeed * Time.deltaTime;

            SetObjectAlpha(fadeAlpha);

            // if (fadeAlpha <= 0f)
            // {
                // Destroy(gameObject);
            // }
        }
    }

    private void SetObjectAlpha(float alpha)
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            if (renderer != null)
            {
                Material material = renderer.material;

                if (material != null)
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }
        }

    }
}
