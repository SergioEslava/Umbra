using TheKiwiCoder;
using UnityEngine;

[System.Serializable]
public class RandomSelection : CompositeNode
{

    private int m_randomNodeIndex = 0;

    protected override void OnStart()
    {
        m_randomNodeIndex = Random.Range(0, children.Count);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return children[m_randomNodeIndex].Update();
    }
}