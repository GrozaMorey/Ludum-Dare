using UnityEngine;

namespace Core.Units.State
{
    public class PursuitState : BaseState<UnitView>
    {
        private const float SqrAttackRange = 0.25f;

        public PursuitState(UnitView unit, IStateMachine<UnitView> stateMachine) : base(unit, stateMachine)
        {
        
        }

        private void OnTargetDied()
        {
            StateMachine.SwitchState<WanderState>();
        }

        private bool ReachedTarget(out Vector2 direction)
        {
            direction = Unit.Target.transform.position - Unit.transform.position;
            return direction.sqrMagnitude <= SqrAttackRange;
        }

        public override void Enter()
        {
            Unit.Target.Died += OnTargetDied;
        }
        public override void Exit()
        {
            Unit.Target.Died -= OnTargetDied;
        }
        public override void Update()
        {
            if (!ReachedTarget(out Vector2 direction))
            {
                Unit.Translate(direction.normalized);
            }
            else
            {
                StateMachine.SwitchState<AttackState>();
            }
        }
    }
}