using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Assets.Scripts
{
    public class UnitBattle : MonoBehaviour
    {

        GameObject _PlayerUnitGameObject;
        GameObject _EnemyUnitGameObject;

        private State state;
        private Vector3 slideTargetPosition;
        private Action onSlideComplete;
        private Action onAttackComplete;

        private enum State
        {
            Idle,
            Sliding,
            Busy,
        }

        //private CrewMembers Unit_CrewMember;
        private Animator anim;

        private void Awake()
        {
            //_PlayerUnitGameObject = new GameObject();
            //_EnemyUnitGameObject = new GameObject();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Busy:
                    
                    break;
                case State.Sliding:
                    float slideSpeed = 2f;
                    transform.position += (slideTargetPosition - transform.position) * slideSpeed * Time.deltaTime;

                    float reachedDistance = 1f;
                    if (Vector3.Distance(transform.position, slideTargetPosition) < reachedDistance)
                    {
                        // Arrived at Slide Target Position
                        //transform.position = slideTargetPosition;
                        Debug.Log("OnSlideComplete !");
                        onSlideComplete();
                    }
                    break;
            }
        }

        public void Attack(int _unitPosition, int _enemyPosition) // Action onAttackComplete
        {
            GameManager.PlayerCrew.TryGetValue(_unitPosition, out _PlayerUnitGameObject);
            GameManager.EnemyCrew.TryGetValue(_enemyPosition, out _EnemyUnitGameObject);

            Vector3 slideTargetPosition = _EnemyUnitGameObject.transform.position + (transform.position - _EnemyUnitGameObject.transform.position).normalized * 1f; // Offset
            Vector3 startingPosition = transform.position;

            // Slide to Target
            SlideToPosition(slideTargetPosition, () =>
            {
                // Arrived at Target, attack him
                state = State.Busy;
                Vector3 attackDir = (_EnemyUnitGameObject.transform.position - transform.position).normalized;
                PlayAnimAttack(attackDir,() => { 
                    SlideToPosition(startingPosition, () => {
                    // Slide back completed, back to idle
                    state = State.Idle;
                    anim.SetBool("isUnitAttackTurn", false);
                    anim.SetBool("isUnitRunningAttack", false);
                    });
                });
            });
        }

        private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete)
        {
            this.slideTargetPosition = slideTargetPosition;
            this.onSlideComplete = onSlideComplete;
            state = State.Sliding;
            if (slideTargetPosition.x > 0)
            {
                anim.SetBool("isUnitAttackTurn", true);
                //characterBase.PlayAnimSlideRight();
            }
            else
            {
                anim.SetBool("isUnitAttackTurn", true);
                //characterBase.PlayAnimSlideLeft();
            }
        }

        public void PlayAnimAttack(Vector3 attackDir, Action onAttackComplete)
        {
            this.onAttackComplete = onAttackComplete;
            anim.SetBool("isUnitRunningAttack", true);        
        }

        public void AttackEnded()
        {
            onAttackComplete();
        }
    }
}