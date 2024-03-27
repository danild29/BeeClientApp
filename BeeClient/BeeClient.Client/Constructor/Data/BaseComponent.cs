using B2bContructor.Client.Ctor.Data;
using BeeClient.Client.Constructor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace B2bContructor.Client.Ctor.Pages;

public class BaseComponent<T> : ComponentBase where T : BaseElement
{
    [CascadingParameter] public virtual Constructor Constr { get; set; }

    [Parameter] public virtual T Element { get; set; }
    protected string cursor = "pointer";
    protected string dropClass = "";

    public virtual string Top
    {
        get => Element.Top + "px";
        set
        {
            if (value.EndsWith("px"))
            {
                Element.Top = int.Parse(value.Substring(0, value.Length - 2));
            }
        }
    }
    public virtual string Left
    {
        get => Element.Left + "px";
        set
        {
            if (value.EndsWith("px"))
            {
                Element.Left = int.Parse(value.Substring(0, value.Length - 2));
            }
        }
    }
    public virtual string Width
    {
        get => Element.Width + "px";
        set
        {
            if (value.EndsWith("px"))
            {
                Element.Width = int.Parse(value.Substring(0, value.Length - 2));
            }
        }
    }
    public virtual string Height
    {
        get => Element.Height + "px";
        set
        {
            if (value.EndsWith("px"))
            {
                Element.Height = int.Parse(value.Substring(0, value.Length - 2));
            }
        }
    }


    protected virtual void OnDragStartElemnt(DragEventArgs e)
    {
        Constr.Payload = Element;
        cursor = "grabbing";
        dropClass = "dragging-element";
    }

    protected virtual async Task OnDragEndElement(DragEventArgs e)
    {
        Constr.Payload = null;
        cursor = "pointer";
        dropClass = "";
        await OnClick();
    }

    protected virtual async Task OnClick()
    {
        await Constr.ElementCliked(this);
    }

}
