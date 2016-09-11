using UnityEngine;
using System.Collections.Generic;

public class CombatView : MonoBehaviour {
    static int swidth, sheight;
    public int width, height;
    public MapCreatorView mapCreator;

    void Awake()
    {
        swidth = width;
        sheight = height;
        enemyPositions = CreateCharacterPositions(GetStartingPositionForEnemy);
        playerPositions = CreateCharacterPositions(GetStartingPositionForPlayer);
    }

	void Start () {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var props = mapCreator.CreateTileForPosition(x, y, MapCreatorView.TileType.Ground);
                props.baseSprite.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Combat"));
                props.garnishSprite.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Combat"));
            }
        }
	}

    public class CharacterPositions {
        public List<Vector3> meleePositions = new List<Vector3>();
        public List<Vector3> rangedPositions = new List<Vector3>();
    }
    static CharacterPositions enemyPositions;
    static CharacterPositions playerPositions;

    static CharacterPositions GetCharacterPositions(Faction f)
    {
        if (f == Faction.Player)
            return playerPositions;
        else
            return enemyPositions;
    }

    public static void PlaceCharacters(List<CombatController> controllers, Faction f)
    {
        var positions = GetCharacterPositions(f);
        int meleeIndex = 0;
        int rangedIndex = 0;
        controllers.ForEach(c =>
        {
            Vector3 pos;
            if(c.GetCharacter().IsInMelee)
            {
                pos = positions.meleePositions[meleeIndex];
                c.GetCharacter().positionIndex = meleeIndex;
                meleeIndex++;
            }
            else
            {
                pos = positions.rangedPositions[rangedIndex];
                c.GetCharacter().positionIndex = rangedIndex;
                rangedIndex++;
            }

            c.artGO.SetLayerRecursively(LayerMask.NameToLayer("Combat"));
            c.SetWorldPosition(pos);
        });
    }

    public static List<Vector3> GetNewPositions(List<Character> alreadyPositioned, List<Character> needToMove, Faction f)
    {
        var positions = GetCharacterPositions(f);
        HashSet<int> meleeIndicesInUse = new HashSet<int>();
        HashSet<int> rangedIndicesInUse = new HashSet<int>();
        alreadyPositioned.ForEach(c =>
        {
            if (c.IsInMelee)
                meleeIndicesInUse.Add(c.positionIndex);
            else
                rangedIndicesInUse.Add(c.positionIndex);
        });

        List<Vector3> newPositions = new List<Vector3>();
        needToMove.ForEach(c =>
        {
            if (c.IsInMelee)
            {
                for(int i = 0; i < GlobalVariables.maxCombatantsOnTeam; i++)
                {
                    if(!meleeIndicesInUse.Contains(i))
                    {
                        newPositions.Add(positions.meleePositions[i]);
                        c.positionIndex = i;
                        meleeIndicesInUse.Add(i);
                        break;
                    }
                }
            }
            else
            {
                for(int i = 0; i < GlobalVariables.maxCombatantsOnTeam; i++)
                {
                    if(!rangedIndicesInUse.Contains(i))
                    {
                        newPositions.Add(positions.rangedPositions[i]);
                        c.positionIndex = i;
                        rangedIndicesInUse.Add(i);
                        break;
                    }
                }
            }
        });

        return newPositions;
    }

    static CharacterPositions CreateCharacterPositions(System.Func<bool, Vector3> startingPosGetter) {
        var c = new CharacterPositions();
        for(int i = 0; i < GlobalVariables.maxCombatantsOnTeam; i++)
        {
            var offset = GetIndexPositionOffset(i);
            c.meleePositions.Add(startingPosGetter(true) + offset);
            c.rangedPositions.Add(startingPosGetter(false) + offset);
        }
        return c;
    }

    static int GetHeightGradient() { return sheight / 5; }

    static Vector3 GetStartingPositionForPlayer(bool isInMelee)
    {
        return Grid.GetCharacterWorldPositionFromGridPositon(swidth / 2, GetHeightGradient()) + (isInMelee? (-Grid.Get1YMove() * 1.25f) : Vector3.zero);
    }

    static Vector3 GetIndexPositionOffset(int index)
    {
        int indexBasedOffset = 0;
        if (index % 2 > 0)
            indexBasedOffset = (index + 1) / 2;
        else
            indexBasedOffset = -((index + 1) / 2);

        return Grid.Get1XMove() * indexBasedOffset * 1.25f;
    }

    static Vector3 GetStartingPositionForEnemy(bool isInMelee)
    {
        return Grid.GetCharacterWorldPositionFromGridPositon(swidth / 2, GetHeightGradient() * 3) - (Grid.Get1YMove() * 0.5f) + (isInMelee? (Grid.Get1YMove() * 1.25f) : Vector3.zero);
    }
}
