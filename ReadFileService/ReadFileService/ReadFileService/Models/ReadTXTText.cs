using System.Text.RegularExpressions;

namespace ReadFileService.Models
{
    public class ReadTXTText : AbstractFileProcessing<Dictionary<string, int>>
    {
        public override ReturnInfo<Dictionary<string, int>> ProcessText(string fileText)
        {
            try
            {
                Dictionary<string, int> result = new Dictionary<string, int>();

                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                fileText = rgx.Replace(fileText, "");
                string[] words = fileText.Split(null);
                if (words.Length == 0)
                {
                    return new(result);
                }
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] != "" && !result.TryAdd(words[i], 1))
                    {
                        result[words[i]]++;
                    }
                }
                return new(result);
            }
            catch (Exception ex)
            {
                return new(new HttpError(500, "Exception while parsing file!"));
            }
        }

        public override async Task<ReturnInfo<string>> ReadText(string fileName)
        {
            try
            {
                var text = await
                    File.ReadAllTextAsync(".\\Files\\" + fileName);
                return new(text);
            }
            catch (Exception ex)
            {
                return new(new HttpError(404, "Can't find file"));
            }
        }
    }
}