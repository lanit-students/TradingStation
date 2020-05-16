using System;

namespace GUI.Scripts
{
    public class AvatarGetter
    {
        public static string GetUserImage(byte[] avatar, string extension)
        {
            var base64 = Convert.ToBase64String(avatar);
            return String.Format("data:image/{0};base64,{1}", extension, base64);
        }
    }
}
