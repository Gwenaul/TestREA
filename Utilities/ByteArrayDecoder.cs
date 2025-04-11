using System.Text;

namespace TestREA.Utilities;

public static class ByteArrayDecoder
{
    public static string DecodeByteArrayToString(byte[] byteArray)
    {
        // Vérifier si le tableau est null ou vide
        if (byteArray == null || byteArray.Length == 0)
        {
            return string.Empty;
        }

        // Convertir le tableau d'octets en chaîne UTF-8
        return Encoding.UTF8.GetString(byteArray);
    }
}