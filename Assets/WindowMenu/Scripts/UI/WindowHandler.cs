using UnityEngine;

public class WindowHandler : MonoBehaviour
{

    // Panels
    public GameObject panelTitle;
    public GameObject panelBody;

    public GameObject panelRightBorder;
    public GameObject panelBottomBorder;
    public GameObject panelBottomRightBorder;


    // Offset cursor position
    private Vector3 offsetPosition;


    // Window size
    public bool isResizable = true;
    public Vector2 minimumSize = new Vector2(50, 50);
    public Vector2 maximumSize = new Vector2(1000, 1000);
    public Vector2 initialSize = new Vector2(50, 50);

    // Custom cursors
    public Texture2D DefaultCursorTexture;
    public Texture2D VerticalCursorTexture;
    public Texture2D HorizontalCursorTexture;
    public Texture2D DiagonalCursorTexture;


    // Use this for initialization
    void Start()
    {

        // Set the offset cursor position on panel title click
        panelTitle.GetComponent<Draggable>().SetActionOnClick(() => { offsetPosition = transform.position - Input.mousePosition; });

        // Prevent maximum size values
        maximumSize.x = Mathf.Max(minimumSize.x, maximumSize.x);
        maximumSize.y = Mathf.Max(minimumSize.y, maximumSize.y);

        // Prevent the initials size values
        initialSize.x = Mathf.Max(minimumSize.x, initialSize.x);
        initialSize.y = Mathf.Max(minimumSize.y, initialSize.y);

        initialSize.x = Mathf.Min(maximumSize.x, initialSize.x);
        initialSize.y = Mathf.Min(maximumSize.y, initialSize.y);

        // Set the initials size values
        panelBody.GetComponent<RectTransform>().sizeDelta = initialSize;
        Vector2 titleSize = panelTitle.GetComponent<RectTransform>().sizeDelta;
        panelTitle.GetComponent<RectTransform>().sizeDelta = new Vector2(initialSize.x, titleSize.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Dragging handler
        bool isDrag = false;

        if (panelTitle.GetComponent<Draggable>().isDragging())
        {
            SetPosition();
            isDrag = true;
        }

        if (isResizable)
        {
            if (panelRightBorder.GetComponent<Draggable>().isDragging())
            {
                ResizeWidth();
                isDrag = true;
            }

            else if (panelBottomBorder.GetComponent<Draggable>().isDragging())
            {
                ResizeHeight();
                isDrag = true;
            }

            else if (panelBottomRightBorder.GetComponent<Draggable>().isDragging())
            {
                ResizeWidth();
                ResizeHeight();
                isDrag = true;
            }


            // Overlapping handler (for cursors)

            if (panelRightBorder.GetComponent<Draggable>().isOverlapping())
            {
                if (HorizontalCursorTexture != null)
                    Cursor.SetCursor(HorizontalCursorTexture, new Vector2(HorizontalCursorTexture.width / 2, HorizontalCursorTexture.height / 2), CursorMode.Auto);
            }

            else if (panelBottomBorder.GetComponent<Draggable>().isOverlapping())
            {
                if (VerticalCursorTexture != null)
                    Cursor.SetCursor(VerticalCursorTexture, new Vector2(VerticalCursorTexture.width / 2, VerticalCursorTexture.height / 2), CursorMode.Auto);
            }

            else if (panelBottomRightBorder.GetComponent<Draggable>().isOverlapping())
            {
                if (DiagonalCursorTexture != null)
                    Cursor.SetCursor(DiagonalCursorTexture, new Vector2(DiagonalCursorTexture.width / 2, DiagonalCursorTexture.height / 2), CursorMode.Auto);

            }
            else if (!isDrag)
            {
                Cursor.SetCursor(DefaultCursorTexture, Vector2.zero, CursorMode.Auto);
            }
        }

        CheckWindowOutside();

    }


    /**
	 * Check if the window is outside of the canvas.
	 */
    private void CheckWindowOutside()
    {
        Vector2 pos = transform.GetComponent<RectTransform>().position;
        Vector2 canvasSize = transform.GetComponent<RectTransform>().GetComponentsInParent<Canvas>()[0].GetComponent<RectTransform>().sizeDelta;
        float height = canvasSize.y - panelTitle.GetComponent<RectTransform>().sizeDelta.y;
        float width = canvasSize.x - panelTitle.GetComponent<RectTransform>().sizeDelta.x;

        Vector2 finalPos = pos;

        if (pos.y <= 0)
        {
            finalPos.y = 0;
        }

        else if (pos.y >= height)
        {
            finalPos.y = height;
        }

        if (pos.x < 0)
        {
            finalPos.x = 0;
        }

        else if (pos.x > width)
        {
            finalPos.x = width;
        }

        transform.GetComponent<RectTransform>().position = finalPos;
    }



    /*
	 *  Move and Resize the window
	 */

    private void SetPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        transform.SetPositionAndRotation(mousePos + offsetPosition, Quaternion.identity);
    }

    private void ResizeWidth()
    {
        Vector3 mousePos = Input.mousePosition;
        float width = (mousePos.x - transform.position.x);

        Vector2 currentSize = panelTitle.GetComponent<RectTransform>().sizeDelta;
        Vector2 currentBodySize = panelBody.GetComponent<RectTransform>().sizeDelta;

        // Set the size
        if (width > minimumSize.y && width < maximumSize.y)
        {
            panelTitle.GetComponent<RectTransform>().sizeDelta = new Vector2(width, currentSize.y);
            panelBody.GetComponent<RectTransform>().sizeDelta = new Vector2(width, currentBodySize.y);
        }
    }


    private void ResizeHeight()
    {
        Vector3 mousePos = Input.mousePosition;
        float height = (transform.position.y - mousePos.y);

        Vector2 currentBodySize = panelBody.GetComponent<RectTransform>().sizeDelta;

        // Set the size
        if (height > minimumSize.x && height < maximumSize.x)
            panelBody.GetComponent<RectTransform>().sizeDelta = new Vector2(currentBodySize.x, height);
    }

    public void ToggleVisibility()
    {
        bool toogle = !panelBody.active;
        panelBody.SetActive(toogle);
    }

    public void SetVisibility(bool visible)
    {
        panelBody.SetActive(visible);
    }

    public bool IsVisible()
    {
        return panelBody.active;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
