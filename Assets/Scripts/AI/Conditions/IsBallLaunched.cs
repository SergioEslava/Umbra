
using TheKiwiCoder;

class IsBallLaunched : ConditionNode
{

    BehaviourTreeInstance m_behaviourTreeInstance;
    private BlackboardKey<CompanionDogSM> m_sharedMemoryKey;

    protected override void OnStart()
    {
        m_behaviourTreeInstance = context.GetComponent<BehaviourTreeInstance>();
        m_sharedMemoryKey = m_behaviourTreeInstance.FindBlackboardKey<CompanionDogSM>("shared memory");
    }

    protected override bool CheckCondition()
    {
        return m_sharedMemoryKey.value.BallObject.isLaunched;
    }
}
