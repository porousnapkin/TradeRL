using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapTownRegistry {
    Dictionary<Vector2, Town> positionToTown = new Dictionary<Vector2, Town>();
    Dictionary<Town, HashSet<Vector2>> townToPositions = new Dictionary<Town, HashSet<Vector2>>();
    const int TownOwnershipDistance = 20;

    public Town GetTownForPosition(Vector2 position)
    {
        if(positionToTown.ContainsKey(position))
            return positionToTown[position];
        return null;
    }

    public List<Vector2> GetPositionsOwnedByTown(Town t)
    {
        if(townToPositions.ContainsKey(t))
            return townToPositions[t].ToList();
        return new List<Vector2>();
    }

    public void SetupMapForTown(Town t)
    {
        var worldPosition = t.worldPosition;
        for(int x = -TownOwnershipDistance; x <= TownOwnershipDistance; x++)
            for (int y = -TownOwnershipDistance; y <= TownOwnershipDistance; y++)
                AttemptToAddPositionToTown(t, worldPosition + new Vector2(x, y));
    }

    void AttemptToAddPositionToTown(Town t, Vector2 position)
    {
        if (IsPositionOwnedByACloserTown(t, position))
            return;

        positionToTown[position] = t;

        if (!townToPositions.ContainsKey(t))
            townToPositions[t] = new HashSet<Vector2>();
        townToPositions[t].Add(position);
    }

    bool IsPositionOwnedByACloserTown(Town t, Vector2 position)
    {
        return positionToTown.ContainsKey(position) &&
            Vector2.Distance(positionToTown[position].worldPosition, position) <= Vector2.Distance(t.worldPosition, position);
    }
}
