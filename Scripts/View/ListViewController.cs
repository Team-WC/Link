using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ListViewController : ListView
{
    public Scrollbar scrollbar;

    protected override float contentAnchoredPosition { get { return -this._contentRect.anchoredPosition.y; } set { this._contentRect.anchoredPosition = new Vector2(this._contentRect.anchoredPosition.x, -value); } }
    protected override float contentSize { get { return this._contentRect.rect.height; } }
    protected override float viewportSize { get { return this._viewportRect.rect.height; } }

    private HorizontalScrollSnap timeline;

    protected override void Awake()
    {
        base.Awake();
        this._itemSize = this.itemPrototype.rect.height;

        timeline = GameObject.Find("Timeline").GetComponent<HorizontalScrollSnap>();
    }

    public void DisplayItems(int[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            InsertItem(keys[i]);
        }
        Refresh();
        ScrollTop();
    }

    public void DisplayItem(int key)
    {
        InsertItem(key);
        Refresh();
        ScrollTop();

        // Badge control
        if (string.Equals(gameObject.name, "Posts") && timeline.CurrentPage != 0)
        {
            BadgeController.instance.BadgeOn(0);
        }
        else if (string.Equals(gameObject.name, "Users") && timeline.CurrentPage != 1)
        {
            BadgeController.instance.BadgeOn(1);
        }
        else if (string.Equals(gameObject.name, "Alarms") && timeline.CurrentPage != 2)
        {
            BadgeController.instance.BadgeOn(2);
        }
        else if (string.Equals(gameObject.name, "Messages") && timeline.CurrentPage != 3)
        {
            BadgeController.instance.BadgeOn(3);
        }
    }

    public void ListClear()
    {
        totalItemCount = 0;
        totalItemSize = 0;

        _containers.Clear();

        Component[] items = this.gameObject.GetComponentsInChildren<Component>();
        for (int i = 0; i < items.Length; i++)
        {
            if (string.Equals(items[i].name, "item"))
                Destroy(items[i].gameObject);
        }

        Refresh();
        ScrollTop();
    }

    public void ScrollTop()
    {
        StartCoroutine(ScrollTopCo());
    }

    private IEnumerator ScrollTopCo()
    {
        yield return null;
        scrollbar.value = 1;
    }
}
