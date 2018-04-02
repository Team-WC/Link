/// @author mosframe / https://github.com/mosframe
/// Modified by chanh327 / https://github.com/chanh327

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// List View
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public abstract class ListView : UIBehaviour
{
    public enum InsertType
    {
        InsertTop,
        InsertBottom
    }

    protected int totalItemCount = 0;
    public RectTransform itemPrototype = null;

    protected abstract float contentAnchoredPosition { get; set; }
    protected abstract float contentSize { get; }
    protected abstract float viewportSize { get; }

    protected LinkedList<RectTransform> _containers = new LinkedList<RectTransform>();
    protected float _prevAnchoredPosition = 0;
    protected float _itemSize = -1;
    protected float totalItemSize = 0;

    protected RectTransform _viewportRect = null;
    protected RectTransform _contentRect = null;

    public InsertType insertType;

    // Awake
    protected override void Awake()
    {
        if (this.itemPrototype == null)
        {
            Debug.LogError(new { this.name, this.itemPrototype });
            return;
        }

        var scrollRect = this.GetComponent<ScrollRect>();
        this._viewportRect = scrollRect.viewport;
        this._contentRect = scrollRect.content;
    }

    // Start
    protected override void Start()
    {
        this.itemPrototype.gameObject.SetActive(false);
    }

    public void InsertItem(int index)
    {
        var itemRect = Instantiate(this.itemPrototype);
        itemRect.SetParent(this._contentRect, false);
        itemRect.name = "item";

        this.UpdateItem(index, itemRect.gameObject);

        totalItemSize += itemRect.getSize().y;
        totalItemCount++;

        if (insertType == InsertType.InsertTop)
        {
            this._containers.AddLast(itemRect);
        }
        else
        {
            this._containers.AddFirst(itemRect);
        }
        itemRect.gameObject.SetActive(true);
    }

    public void InsertSpace(float space)
    {
        totalItemSize += space;
    }

    // refresh
    public void Refresh()
    {
        var pos = totalItemSize;
        foreach (var itemRect in this._containers)
        {
            // set item position
            pos -= itemRect.getSize().y;
            itemRect.anchoredPosition = new Vector2(0, -pos);
        }

        ResizeContent();
    }

    // resize content
    private void ResizeContent()
    {
        var size = this._contentRect.getSize();
        size.y = totalItemSize;
        this._contentRect.setSize(size);
    }

    // update item
    private void UpdateItem(int index, GameObject itemObj)
    {
        itemObj.SetActive(true);

        var item = itemObj.GetComponent<IViewItem>();
        if (item != null)
            item.OnUpdateItem(index);
    }
}
