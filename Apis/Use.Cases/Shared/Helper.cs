namespace ManagementProducts.Use.Cases.Shared
{
    public static class Helper
    {
        public static (byte[] data, string type) GetByteMultimedia(string data)
        {
            try
            {
                string multimedia = data.Split("base64,")[1];
                string type = data.Split("base64,")[0].Replace("data:", "").Replace(";", "");
                return (System.Convert.FromBase64String(multimedia), type);
            }
            catch (Exception ex)
            {
                return (null, null);
            }

        }
    }
}
