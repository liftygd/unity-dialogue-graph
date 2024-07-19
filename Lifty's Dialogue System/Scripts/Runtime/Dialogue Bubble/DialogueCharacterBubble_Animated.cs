using System;
using System.Collections;
using UnityEngine;

namespace Lifty.DialogueSystem
{
    public class DialogueCharacterBubble_Animated : DialogueCharacterBubbleBase
    {
        [Header("Animation")] 
        [SerializeField] protected Animator _anim;
        [SerializeField] protected float _appearAnimationTime;
        protected bool _active;

        private void Start()
        {
            textUI.text = "";
        }

        public override void Show(DialogueTextData textData, Action callback)
        {
            if (!_active)
            {
                _active = true;
                _anim.SetTrigger("Appear");
            }
            
            base.Show(textData, callback);
        }

        protected override IEnumerator PrintTextData(DialogueTextData textData, Action callback)
        {
            yield return new WaitForSeconds(_appearAnimationTime);

            yield return base.PrintTextData(textData, callback);
        }

        public override void Hide()
        {
            textUI.text = "";
            
            if (_active)
            {
                _active = false;
                _anim.SetTrigger("Disappear");
            }
        }
    }
}
