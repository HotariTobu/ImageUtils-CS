foreach (var path in Environment.GetCommandLineArgs().Skip(1))
{
    Console.WriteLine(path);
    try
    {
        using var bitmap = new Bitmap(path);
        foreach (var item in bitmap.PropertyItems)
        {
            if (item.Type == 2)
            {
                string val = System.Text.Encoding.ASCII.GetString(item.Value);
                val = val.Trim(new char[] { '\0' });
                Console.WriteLine("{0:X}:{1}:{2}", item.Id, item.Type, val);
            }
            else
            {
                Console.WriteLine("{0:X}:{1}:{2}", item.Id, item.Type, item.Len);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
    Console.WriteLine();
}