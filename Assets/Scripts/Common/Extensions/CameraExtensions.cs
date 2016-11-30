﻿using UnityEngine;

namespace GameEngineering.Common.Extensions
{
    public static class CameraExtensions
    {
        /// <summary>
        /// Returns a Bounds object with the size of the orthographic camera
        /// </summary>
        public static Bounds OrthographicBounds(this Camera camera)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                camera.transform.position - new Vector3(0, 0, camera.transform.position.z),
                new Vector3(cameraHeight * screenAspect, cameraHeight, 2));
            return bounds;
        }
    }
}