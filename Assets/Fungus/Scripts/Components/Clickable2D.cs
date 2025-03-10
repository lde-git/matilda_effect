// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus
{
    /// <summary>
    /// Detects mouse clicks and touches on a Game Object, and sends an event to all Flowchart event handlers in the scene.
    /// The Game Object must have a Collider or Collider2D component attached.
    /// Use in conjunction with the ObjectClicked Flowchart event handler.
    /// </summary>
    public class Clickable2D : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("Is object clicking enabled")]
        [SerializeField] protected bool clickEnabled = true;

        [Tooltip("Mouse texture to use when hovering mouse over object")]
        [SerializeField] protected Texture2D hoverCursor;

        [Tooltip("Use the UI Event System to check for clicks. Clicks that hit an overlapping UI object will be ignored. Camera must have a PhysicsRaycaster component, or a Physics2DRaycaster for 2D colliders.")]
        [SerializeField] protected bool useEventSystem;

        [Tooltip("Color of the outline when hovering")]
        [SerializeField] protected Color outlineColor = Color.yellow;

        [Tooltip("Width of the outline")]
        [SerializeField] protected float outlineWidth = 0.02f;


        private PolygonCollider2D polygonCollider;
        private LineRenderer lineRenderer;


        public void Update() {

            lineRenderer.widthMultiplier = outlineWidth;
        }
        
        protected virtual void Start()
        {
            polygonCollider = GetComponent<PolygonCollider2D>();
            if (polygonCollider == null)
            {
                Debug.LogError("Outline script requires a PolygonCollider2D component.", this);
                return;
            }

            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            /*var shader = Shader.Find("Custom/BlurredLineRenderer");
            if (shader == null) Debug.Log("Shader doesnt exist");
            */
            Material myMaterial = Resources.Load<Material>("outlineMaterial");
            lineRenderer.material = myMaterial;
            lineRenderer.widthMultiplier = outlineWidth;
            lineRenderer.positionCount = polygonCollider.points.Length + 1; // +1 to close the loop
            lineRenderer.loop = true;
        }

        protected virtual void ChangeCursor(Texture2D cursorTexture)
        {
            if (!clickEnabled)
            {
                return;
            }

            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        protected virtual void DoPointerClick()
        {
            if (!clickEnabled)
            {
                return;
            }

            var eventDispatcher = FungusManager.Instance.EventDispatcher;

            eventDispatcher.Raise(new ObjectClicked.ObjectClickedEvent(this));
        }

        protected virtual void ToggleOutline(bool showOutline)
        {
            if (lineRenderer != null)
            {
                lineRenderer.enabled = showOutline;
                if (showOutline)
                {
                    lineRenderer.startColor = outlineColor;
                    lineRenderer.endColor = outlineColor;

                    Vector2[] positions2D = GetColliderPositions();
                    // Convert Vector2[] to Vector3[]
                    Vector3[] positions3D = System.Array.ConvertAll<Vector2, Vector3>(positions2D, v => v);

                    lineRenderer.SetPositions(positions3D);
                }
            }
        }


        private Vector2[] GetColliderPositions()
        {
            Vector2[] points = polygonCollider.points;
            Vector2[] positions = new Vector2[points.Length + 1]; // +1 to close the loop

            for (int i = 0; i < points.Length; i++)
            {
                positions[i] = points[i] + polygonCollider.offset;
            }

            // Close the loop by adding the first point again
            positions[points.Length] = positions[0];

            return positions;
        }

        protected virtual void DoPointerEnter()
        {
            ChangeCursor(hoverCursor);
            ToggleOutline(true);
        }

        protected virtual void DoPointerExit()
        {
            // Always reset the mouse cursor to be on the safe side
            SetMouseCursor.ResetMouseCursor();
            ToggleOutline(false);
        }

        #region Legacy OnMouseX methods

        protected virtual void OnMouseDown()
        {
            if (!useEventSystem)
            {
                DoPointerClick();
            }
        }

        protected virtual void OnMouseEnter()
        {
            if (!useEventSystem)
            {
                DoPointerEnter();
            }
        }

        protected virtual void OnMouseExit()
        {
            if (!useEventSystem)
            {
                DoPointerExit();
            }
        }

        #endregion

        #region Public members

        /// <summary>
        /// Is object clicking enabled.
        /// </summary>
        public bool ClickEnabled { set { clickEnabled = value; } }

        #endregion

        #region IPointerClickHandler implementation

        // Removed 'public'
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoPointerClick();
            }
        }

        #endregion

        #region IPointerEnterHandler implementation

        // Removed 'public'
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoPointerEnter();
            }
        }

        #endregion

        #region IPointerExitHandler implementation

        // Removed 'public'
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (useEventSystem)
            {
                DoPointerExit();
            }
        }

        #endregion
    }
}