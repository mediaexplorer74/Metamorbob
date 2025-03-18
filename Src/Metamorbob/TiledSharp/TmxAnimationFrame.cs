
// Type: TiledSharp.TmxAnimationFrame




using System.Xml.Linq;


namespace TiledSharp
{
  public class TmxAnimationFrame
  {
    public int Id { get; private set; }

    public int Duration { get; private set; }

    public TmxAnimationFrame(XElement xFrame)
    {
      this.Id = (int) xFrame.Attribute((XName) "tileid");
      this.Duration = (int) xFrame.Attribute((XName) "duration");
    }
  }
}
