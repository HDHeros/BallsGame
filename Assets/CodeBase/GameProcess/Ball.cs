using UnityEngine;

namespace CodeBase.GameProcess
{
    public class Ball : RoundItem
    {
        protected override void Update()
        {
            base.Update();
            Vector3 newPosition = Transform.position;
            newPosition.y += Speed * Time.deltaTime;
            Transform.position = newPosition;
        }
    }
}