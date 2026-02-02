using UnityEngine;

[CreateAssetMenu(fileName = "Companion Dog Shared Memory", menuName = "AI Shared Memory/Companion Dog")]

public class CompanionDogSM : ScriptableObject
{
    private Ball m_ball = new Ball();

    public Ball BallObject { get => m_ball; set => m_ball = value; }

    
}

public class Ball
{
    public GameObject gameObject;
    public bool isLaunched;
    public Vector3 destination = Vector3.zero;
}
