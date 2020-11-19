using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class RoyaleArena : IArena
{
    public Dictionary<int, Battlecard> byId;
    public Dictionary<string, HashSet<Battlecard>> byName;
    public HashSet<Battlecard> battlecards;

    public RoyaleArena()
    {
        byId = new Dictionary<int, Battlecard>();
        byName = new Dictionary<string, HashSet<Battlecard>>();
        battlecards = new HashSet<Battlecard>();
    }

    public int Count => this.byId.Count;

    public void Add(Battlecard card)
    {
        byId[card.Id] = card;
        battlecards.Add(card);

        if (!byName.ContainsKey(card.Name))
        {
            byName[card.Name] = new HashSet<Battlecard>();
        }

        byName[card.Name].Add(card);
    }

    public void ChangeCardType(int id, CardType type)
    {
        if (!byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        byId[id].Type = type;
    }

    public bool Contains(Battlecard card)
    {
        return byId.ContainsKey(card.Id);
    }

    public IEnumerable<Battlecard> FindFirstLeastSwag(int n)
    {
        if (byId.Count < n) // < 0 ?
        {
            throw new InvalidOperationException();
        }
        return byId.Values.OrderBy(x => x.Swag).ThenBy(x => x.Id).Take(n);
    }

    public IEnumerable<Battlecard> GetAllByNameAndSwag()
    {
        var result = new List<Battlecard>();

        foreach (var kvp in byName)
        {
            if (kvp.Value.Count == 0)
            {
                throw new InvalidOperationException();
            }
            Battlecard maxCard = null;

            foreach (var card in kvp.Value)
            {
                if (maxCard == null || maxCard.Swag < card.Swag)
                {
                    maxCard = card;
                }
            }
            result.Add(maxCard);
        }
        return result;
    }

    public IEnumerable<Battlecard> GetAllInSwagRange(double lo, double hi)
    {
        return byId.Values.Where(x => x.Swag >= lo && x.Swag <= hi).OrderBy(a => a.Swag);
    }

    public IEnumerable<Battlecard> GetByCardType(CardType type)
    {
        var cards = byId.Values
            .Where(x => x.Type == type)
            .OrderByDescending(x => x.Damage)
            .ThenBy(x => x.Id);

        if (cards.Count() == 0)
        {
            throw new InvalidOperationException();
        }
        return cards;
    }

    public IEnumerable<Battlecard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        var result = byId.Values
            .Where(x => x.Type == type && x.Damage <= damage)
            .OrderByDescending(x => x.Damage)
            .ThenBy(x => x.Id);
        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public Battlecard GetById(int id)
    {
        if (!byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        return byId[id];
    }

    public IEnumerable<Battlecard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        var result = byName[name].Where(x => x.Swag >= lo && x.Swag < hi)
            .OrderByDescending(x => x.Swag)
            .ThenBy(x => x.Id);

        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Battlecard> GetByNameOrderedBySwagDescending(string name)
    {
        var result = byId
            .Values
            .Where(x => x.Name == name)
            .OrderByDescending(x => x.Swag)
            .ThenBy(x => x.Id);

        if (result.Count() == 0)
        {
            throw new InvalidOperationException(); //ToDo -> optimization
        }

        return result;
    }

    public IEnumerable<Battlecard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        var result = byId
            .Values
            .Where(x => x.Type == type && x.Damage > lo && x.Damage <= hi)
            .OrderByDescending(x => x.Damage)
            .ThenBy(x => x.Id);

        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public void RemoveById(int id)
    {
        if (!byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        var card = byId[id];
        byId.Remove(id);
        battlecards.Remove(card);
        byName[card.Name].Remove(card);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    public IEnumerator<Battlecard> GetEnumerator()
    {
        foreach (var battlecard in this.battlecards)
        {
            yield return battlecard;
        }
    }
}
