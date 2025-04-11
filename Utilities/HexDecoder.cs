using System;
using System.Text;

namespace TestREA.Utilities
{
    public static class HexDecoder
    {
        public static string DecodeHexString(string hexString)
        {
            // Supprimer le préfixe "0x" s'il est présent
            if (hexString.StartsWith("0x"))
            {
                hexString = hexString.Substring(2);
            }

            // Convertir la chaîne hexadécimale en tableau d'octets
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            // Convertir le tableau d'octets en chaîne de texte
            return Encoding.UTF8.GetString(bytes);
        }
    }
}