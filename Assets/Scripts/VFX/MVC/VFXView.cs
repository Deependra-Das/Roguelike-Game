using UnityEngine;
using System.Collections;

namespace Roguelike.VFX
{
    public class VFXView : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void PlayAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger("PlaySmoke");
            }
            StartCoroutine(DisableObjectAfterAnimation());
        }

        IEnumerator DisableObjectAfterAnimation()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);
            gameObject.SetActive(false);
        }
    }
}