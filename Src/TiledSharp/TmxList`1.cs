// Decompiled with JetBrains decompiler
// Type: TiledSharp.TmxList`1
// Assembly: Metamorbob, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 57634F46-85AB-4950-BD55-5B1B8695C038
// Assembly location: C:\Users\Admin\Desktop\RE\Metamorbob\Metamorbob.exe

using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace TiledSharp
{
  public class TmxList<T> : KeyedCollection<string, T> where T : ITmxElement
  {
    private System.Collections.Generic.Dictionary<string, int> nameCount = new System.Collections.Generic.Dictionary<string, int>();

    public new void Add(T t)
    {
      string name = t.Name;
      if (this.Contains(name))
        ++this.nameCount[name];
      else
        this.nameCount.Add(name, 0);
      base.Add(t);
    }

    protected override string GetKeyForItem(T item)
    {
      string key = item.Name;
      int num = this.nameCount[key];
      int count = 0;
      while (this.Contains(key))
      {
        key = key + string.Concat(Enumerable.Repeat<string>("_", count)) + num.ToString();
        ++count;
      }
      return key;
    }
  }
}
