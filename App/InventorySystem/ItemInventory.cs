using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Orion;

class ItemInventory
{
    // -- Properties / Fields

    private List<Item> _items; // !try List<List<Item>> || Dictionary and setup sorting functions

    private CellContainer _cellContainer;

    private int _uniqueItems;

    // -- Initialization

    public ItemInventory(CellContainer cellContainer)
    {
        _items = new List<Item>();
        _cellContainer = cellContainer;
    }

    // -- Public Interface

    public void AddItem(string name, string imageSrc, int value)
    {
        var item = new Item(name, imageSrc, value);
        _items.Add(item);

        int count = _items.Count(x => x.Name == name);

        _cellContainer.SetItem(imageSrc, name, count);
    }

    public void RemoveItem(string name, int quanity)
    {
        int count = _items.Count(x => x.Name == name);
        int toRemove = count - quanity < 1 ? 1 : count - quanity;

        for (int i = 0; i < quanity; i++)
        {
            var item = _items.First(x => x.Name == name);

            if (_items.Remove(item) == false)
                break;
        }

        _cellContainer.ClearItem(name, toRemove);
    }
}
