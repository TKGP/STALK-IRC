namespace STALK_IRC
{
    class SocData
    {
        public string logPath;
        public int lastLines;
        public int sentMessages;

        public SocData(string setLogPath, int setLastLines)
        {
            logPath = setLogPath;
            lastLines = setLastLines;
            sentMessages = 0;
        }
    }
}
