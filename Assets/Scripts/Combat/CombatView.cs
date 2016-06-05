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
    }

	void Start () {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                mapCreator.CreateTileForPosition(x, y, MapCreatorView.TileType.Ground);
	}

    public static void PositionEnemyCharacters(List<CombatController> controllers)
    {
        PositionCharacters(controllers, GetStartingPositionForEnemy);
    }

    public static void PositionPlayerCharacters(List<CombatController> controllers)
    {
        PositionCharacters(controllers, GetStartingPositionForPlayer);
    }

    static void PositionCharacters(List<CombatController> controllers, System.Func<bool, Vector3> startingPosGetter) {
        int frontRow = 0;
        int backRow = 0;
        foreach(var c in controllers) {
            var index = 0;
            var isInMelee = c.GetCharacter().IsInMelee;
            if(isInMelee)
            {
                index = frontRow;
                frontRow++;
            }
            else
            {
                index = backRow;
                backRow++;
            }

            var position = startingPosGetter(isInMelee) + GetIndexPositionOffset(index);
            c.SetWorldPosition(position);
        }
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
