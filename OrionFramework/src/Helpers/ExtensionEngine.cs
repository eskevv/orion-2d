using System.Linq;

namespace OrionFramework.Helpers;

public static class ExtensionEngine
{
    public static int[] ToIntArray(this string src)
    {
        return src.Split(',').Select(int.Parse).ToArray();
    }

    public static string CombineWithPath(this string src, string path)
    {
        int index = path.LastIndexOf('/');
        return path.Substring(0, index + 1) + src;
    }

    public static string FileName(this string src)
    {
        int findIndex = src.LastIndexOf('/');
        int findExtension = src.LastIndexOf('.');
        int index = findIndex == -1 ? 0 : findIndex + 1;
        int extension = findExtension == -1 ? src.Length : findExtension;

        return src.Substring(index, extension - index);
    }

    public static string RemoveExtension(this string src)
    {
        int findExtension = src.LastIndexOf('.');
        int extension = findExtension == -1 ? src.Length : findExtension;
        return src[..extension];
    }

    public static T[] CombineArrays<T>(this T[] first, T[] second)
    {
        var final = new T[first.Length + second.Length];
        first.CopyTo(final, 0);
        second.CopyTo(final, first.Length);
        return final;
    }
}