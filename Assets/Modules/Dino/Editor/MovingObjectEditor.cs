using UnityEngine;
using UnityEditor;

namespace StansAssets.ProjectSample.Dino.Game
{
    [CustomEditor (typeof(MovingObject))]
    public class MovingObjectInspector : Editor
    {
        /*void OnSceneGUI ()
        {
            Handles.BeginGUI ();
            var tg = (MovingObject)target;

            var rect = ResizeRect (
                                   tg.Bounds,
                                   Handles.CubeHandleCap,
                                   Color.green,
                                   Color.yellow,
                                   HandleUtility.GetHandleSize (tg.transform.position),
                                   .1f);

            tg.Bounds = rect;
            Handles.EndGUI ();
        }*/

        static Rect ResizeRect (Rect rect, Handles.CapFunction capFunc, Color capCol, Color fillCol, float capSize, float snap) {

            Vector3[] rectangleCorners = { 
                                             new Vector3(rect.min.x, rect.min.y, 0), // Bottom Left
                                             new Vector3(rect.max.x, rect.min.y, 0), // Bottom Right
                                             new Vector3(rect.max.x, rect.max.y, 0), // Top Right
                                             new Vector3(rect.min.x, rect.max.y, 0)  // Top Left
                                         }; 

            Handles.color = fillCol;
            Handles.DrawSolidRectangleWithOutline(rectangleCorners, new Color(fillCol.r, fillCol.g, fillCol.b, 0.25f), capCol);

            Vector3[] handlePoints = { 
                                         new Vector3(rect.min.x, rect.center.y, 0), // Left
                                         new Vector3(rect.max.x, rect.center.y, 0), // Right
                                         new Vector3(rect.center.x, rect.min.y, 0), // Bottom 
                                         new Vector3(rect.center.x, rect.max.y, 0)  // Top
                                     }; 

            Handles.color = capCol;

            Vector3 leftHandle =   Handles.Slider(handlePoints[0], Vector3.right, capSize, capFunc, snap);
            leftHandle.x   = Mathf.Min (leftHandle.x,  handlePoints[1].x-0.1f);
            Vector3 rightHandle =  Handles.Slider(handlePoints[1], Vector3.right, capSize, capFunc, snap);
            rightHandle.x  = Mathf.Max (rightHandle.x, handlePoints[0].x+0.1f);
            Vector3 bottomHandle = Handles.Slider(handlePoints[2], Vector3.up,    capSize, capFunc, snap);
            bottomHandle.y = Mathf.Min (bottomHandle.y, handlePoints[3].y-0.1f);
            Vector3 topHandle =    Handles.Slider(handlePoints[3], Vector3.up,    capSize, capFunc, snap);
            topHandle.y    = Mathf.Max (topHandle.y, handlePoints[2].y+0.1f);

            return Rect.MinMaxRect(leftHandle.x, bottomHandle.y, rightHandle.x, topHandle.y); 
        }
    }
}
