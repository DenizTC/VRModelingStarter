

public static class Helpers {

    /// <summary>
    /// Converts a boolean to a float (0 or 1).
    /// </summary>
    /// <returns>0 if [value] equals false, 1 otherwise.</returns>
    public static float BoolToFloat(bool value)
    {
        return (value) ? 1f : 0f;
    }

}
