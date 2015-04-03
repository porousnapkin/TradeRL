using UnityEngine;
using System.Collections;
	

public class SearchPoint
{
	public const int kDefaultWeight = 30;
	public const int kImpassableWeight = -1;
	
	public SearchPoint(int inX, int inY)
	{
		position.x = inX;
		position.y = inY;
		weight = kDefaultWeight;
		
	}
	public Vector2 position;
	public float heuristic;
	public float depth;
	public int weight;
	public Vector2 previousPosition;	
};
