using UnityEngine;

public class KeyboardInput
{
    [Inject] public Updater updater {private get; set;}

    public event System.Action<Vector2> MoveKeyPressed = delegate {};

    [PostConstruct]
    public void PostConstruct()
    {
        updater.OnUpdate += Update;
    }

    private void Update()
    {
        //Respond to relevant key presses.
        if (Input.GetKey(KeyCode.H))
            MoveKeyPressed(new Vector2(-1, -1));
        if (Input.GetKey(KeyCode.L))
            MoveKeyPressed(new Vector2(1, 1));
        if (Input.GetKey(KeyCode.J))
            MoveKeyPressed(new Vector2(1, -1));
        if (Input.GetKey(KeyCode.K))
            MoveKeyPressed(new Vector2(-1, 1));
        if (Input.GetKey(KeyCode.Y))
            MoveKeyPressed(new Vector2(-1, 0));
        if (Input.GetKey(KeyCode.U))
            MoveKeyPressed(new Vector2(0, 1));
        if (Input.GetKey(KeyCode.B))
            MoveKeyPressed(new Vector2(0, -1));
        if (Input.GetKey(KeyCode.N))
            MoveKeyPressed(new Vector2(1, 0));
    }
}
