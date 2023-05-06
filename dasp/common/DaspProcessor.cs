using Newtonsoft.Json;
namespace dasp
{
    public class DaspProcessor
    {
        public static string DaspRequestToString(DaspRequest daspRequest)
        {
            string header = JsonConvert.SerializeObject(daspRequest.DaspHeader);
            string body = JsonConvert.SerializeObject(daspRequest.DaspBody);
            int headerSize = header.Length;
            int bodySize = body.Length;
            string requestString = $"{DaspConstants.DASP_PREFIX}[{headerSize}][{bodySize}]{header}{body}";
            return requestString;
        }
        public static bool TryStringToDaspRequest(string receivedData, out DaspRequest result)
        {
            try
            {
                int prefixIndex = receivedData.IndexOf(DaspConstants.DASP_PREFIX);
                if (prefixIndex < 0)
                {
                    result = null;
                    return false;
                }
                else if (prefixIndex > 0)
                {
                    receivedData = receivedData.Remove(0, prefixIndex);
                }
                int commandSizeIndex = receivedData.IndexOf("[") + 1;
                int bodySizeIndex = receivedData.IndexOf("][") + 2;
                int commandSizeEndIndex = receivedData.IndexOf("]");
                int bodySizeEndIndex = receivedData.IndexOf("]", bodySizeIndex);
                int commandSize = int.Parse(receivedData[commandSizeIndex..commandSizeEndIndex]);
                int bodySize = int.Parse(receivedData[bodySizeIndex..bodySizeEndIndex]);
                string commandJson = receivedData.Substring(bodySizeEndIndex + 1, commandSize);
                string bodyJson = receivedData.Substring(bodySizeEndIndex + 1 + commandSize, bodySize);
                int requestSize = bodySizeEndIndex + 1 + commandSize + bodySize + prefixIndex;
                DaspHeader command = JsonConvert.DeserializeObject<DaspHeader>(commandJson);
                DaspBody body = JsonConvert.DeserializeObject<DaspBody>(bodyJson);
                result = new DaspRequest(command, body, requestSize);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}
