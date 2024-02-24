using B2bContructor.Client.Ctor.Data;
using Microsoft.AspNetCore.Components.Web;

namespace B2bContructor.Client.Ctor.Handler;

public class StretcherHandler
{


    public Func<DragEventArgs, Task> ResizeOut { get; set; }

    public Action<BaseElement> Show {  get; set; }
}
