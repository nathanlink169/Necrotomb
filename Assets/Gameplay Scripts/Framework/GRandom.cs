using Random = UnityEngine.Random;

public static class GRandom
{
    public static int Int(int in_iMin, int in_iMax)
    {
        return Random.Range(in_iMin, in_iMax);
    }

    public static float Float(float in_fMin, float in_fMax)
    {
        return Random.Range(in_fMin, in_fMax);
    }
}