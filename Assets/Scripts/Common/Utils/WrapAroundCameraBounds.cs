//Unity
using UnityEngine;

//App
using GameEngineering.Common.Extensions;

/// <summary>
/// Convenience class/function for moving an object's transform
/// to wrap around the edges of the camera.
/// Requires an orthographic camera, axis-aligned, z+ forward
/// </summary>
///

namespace GameEngineering.Common.Utils
{
    public class WrapAroundCameraBounds : MonoBehaviour
    {
        public void Wrap(Rigidbody2D rigidBody,float push)
        {
            Bounds b = Camera.main.OrthographicBounds();
            if (!b.Contains(transform.position))
            {
                Vector3 direction = (Vector3.zero - transform.position).normalized * push;
                //TODO: take width/height of the object into consideration?
                //Wrap Horizontal
                if (transform.position.x - Camera.main.transform.position.x < -b.extents.x)
                {
                    rigidBody.AddForce(direction, ForceMode2D.Force);
                }
                if (transform.position.x - Camera.main.transform.position.x > b.extents.x)
                {
                    rigidBody.AddForce(direction, ForceMode2D.Force);
                }
                //Wrap Vertical
                if (transform.position.y - Camera.main.transform.position.y < -b.extents.y)
                {
                    rigidBody.AddForce(direction, ForceMode2D.Force);
                }
                if (transform.position.y - Camera.main.transform.position.y > b.extents.y)
                {
                    rigidBody.AddForce(direction, ForceMode2D.Force);
                }
            }
        }
    }
}
