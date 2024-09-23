using UnityEngine;

public static class RandomPosition
{
    public static Vector3 GetRandomPosition(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    public static Vector3 GetUniqueRandomPosition(Bounds bounds, Vector3 excludePosition, float minDistance = 1.0f)
    {
        Vector3 newPosition;
        do
        {
            newPosition = GetRandomPosition(bounds);
        } while (Vector3.Distance(newPosition, excludePosition) < minDistance);

        return newPosition;
    }
}