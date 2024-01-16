namespace CommonLibs;

public static class Common
{
    public static bool CheckXSSInput(string input)
    {
        try
        {
            var listDangerousString = new List<string>
            {
                "<applet", "<body", "<embed", "<frame", "<script", "<frameset", "<html", "<iframe", "<img", "<style",
                "<layer", "<link", "<ilayer", "<meta", "<object", "<h", "<input", "<a", "&lt", "&gt"
            };
            if (string.IsNullOrEmpty(input)) return false;
            foreach (var dangerous in listDangerousString)
            {
                if (input.Trim().ToLower().IndexOf(dangerous) >= 0) return false;
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}